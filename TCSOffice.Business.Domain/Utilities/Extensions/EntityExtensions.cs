using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Utilities.Data;
using TCSOffice.Business.Domain.Utilities.Helpers;

namespace TCSOffice.Business.Domain.Utilities.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// This will updated the supplied entity with the matching key values in the KVPs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T ConstructObjectFromKeyValuePairs<T>(this T entity, List<KeyValuePair<string, string>> values) where T : new()
        {
            var classProperties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var classProperty in classProperties)
            {
                var valueInValues = values.FirstOrDefault(x => x.Key == classProperty.Name).Value;
                if (valueInValues != null) entity.GetType().GetProperty(classProperty.Name).SetValue(entity, valueInValues, null);
                // && propertyInEntity.CanWrite
            }
            return entity;
        }

        /// <summary>
        /// This updates the values in the preexisting entity, with the values in the viewModel.
        /// It leaves the other values (those not present in viewmodel) in the preexisting entity untouched.
        /// Is case Insensitive.
        /// It also maps many-to-many relationships eg RolesIds to Roles{RoleId,UserId}
        /// It will NOT update types with the same name BUT different types eg if you have Roles listOfStrings on dto, it will not map to a Roles ListOfRoles
        /// </summary>
        /// <param name="preexistingEntity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static T MapTo<T>(this Object dto, T preexistingEntity)
        {
            var dtoPropertyInfos = dto.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in dtoPropertyInfos)
            {
                var valueInDto = property.GetValue(dto, null);
                var dtoPropertyType = property.PropertyType.FullName;
                var propertyInEntity = preexistingEntity.GetType().GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                //1. If i. property in the entity ii. can write & iii. of same type
                if (propertyInEntity != null && propertyInEntity.CanWrite)
                {
                    var entityPropertyType = propertyInEntity.PropertyType.FullName;
                    if (entityPropertyType == dtoPropertyType) //only write if the types are the same between the dto & entity eg: both are strings, or orders or customers etc
                        propertyInEntity.SetValue(preexistingEntity, valueInDto, null);
                }

                //2. If i. property doesn't exist in entity BUT ii. the property on dto ends with 'Ids' then maybe its the Ids of a join table
                else if (propertyInEntity == null && property.Name.EndsWith("Ids"))
                {
                    var collectionNameOnDto = property.Name;
                    var collectionNameOnActualEntity = collectionNameOnDto.Replace("Ids", "");
                    var collectionInEntity = preexistingEntity.GetType().GetProperty(collectionNameOnActualEntity, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (collectionInEntity == null) continue;

                    var collectionKey = collectionNameOnDto.Replace("Ids", "").TrimEnd('s') + "Id"; //TODO... use trimEnd incase the word has Ids somewhere else
                    preexistingEntity.UpdateCollection(collectionNameOnActualEntity, collectionKey, (List<int>)valueInDto);
                }
            }

            return preexistingEntity;
        }

        /// <summary>
        /// This creates a new instance of T and then updates the values of T from those in the supplied object
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        public static T MapTo<T>(this Object viewmodel) where T : new()
        {
            var entity = new T();
            return viewmodel.MapTo(entity);
        }

        public static IEnumerable<TOutput> MapToCollection<TSource, TOutput>(this IEnumerable<TSource> source)
            where TOutput : new()
        {
            return MapToCollection<TSource, TOutput>(source, null);
        }

        public static IEnumerable<TOutput> MapToCollection<TSource, TOutput>(this IEnumerable<TSource> source, IEnumerable<TOutput> output, string keyName = "Id") where TOutput : new()
        {
            //1. HANDLE NULL ==============================================================================
            //if source is null, throw an erro!
            if (source == null)
                throw new Exception("The source collection CANNOT be null");

            //if output is null, new-up a new list coz we will add new items into it
            if (output == null)
            {
                output = new List<TOutput>();
            }

            var outputCollection = output as IList<TOutput> ?? output.ToList();
            var sourceCollection = source as IList<TSource> ?? source.ToList();

            //2. HANDLE THE ITEMS IN THE OUTPUT COLLECTION ================================================
            var itemsToRemoveFromOutputCollection = new List<TOutput>();
            foreach (var outputItem in outputCollection)
            {
                //remove the item from the outputCollection if it DOES NOT exist in the source collection
                var outputItemId = outputItem.GetPropertyValue(keyName);
                var predicate = new Predicate(keyName, Operators.Equals, outputItemId);
                var matchingItemsInSource = sourceCollection.GetByPredicate(predicate);
                if (!matchingItemsInSource.Any())
                {
                    itemsToRemoveFromOutputCollection.Add(outputItem);
                }
            }
            //You CANNOT remove the items during the foreach as collection updated during cycle will throw errors.
            //You finally remove from the collection here by keeping a track of who must be eventually removed
            itemsToRemoveFromOutputCollection.ForEach(x => outputCollection.Remove(x));

            //3. HANDLE THE ITEMS IN THE SOURCE COLLECTION ==================================================
            foreach (var sourceItem in sourceCollection)
            {
                var sourceItemId = Convert.ToInt32(sourceItem.GetPropertyValue(keyName));

                //i. if id=0 add it irregardless... as it is a new item
                if (sourceItemId == 0)
                {
                    var newOutputItem = sourceItem.MapTo<TOutput>();
                    outputCollection.Add(newOutputItem);
                }
                else
                {
                    //ii. add if not exist
                    var matchingItemsInOutput = outputCollection.GetByPredicate(new Predicate(keyName, Operators.Equals, sourceItemId)).ToList();
                    if (!matchingItemsInOutput.Any())
                    {
                        var newOutputItem = sourceItem.MapTo<TOutput>();
                        outputCollection.Add(newOutputItem);
                    }
                    else
                    {
                        //iii. update values if already exists
                        var firstMatchingItemInOutput = matchingItemsInOutput.FirstOrDefault();
                        sourceItem.MapTo(firstMatchingItemInOutput);
                    }
                }
            }

            return outputCollection;
        }

        /// <summary>
        /// The updates a collection on an entity by dropping all the items not in the newItemIds list and keeping/adding items in the newItemsIds list
        /// </summary>
        /// <typeparam name="TColType">the type of items in the collection</typeparam>
        /// <param name="item"></param>
        /// <param name="newItemsIds">These are the Ids that should remain in the collection</param>
        /// <param name="collectionName">This is the name of the collection eg Material.Categories </param>
        /// <param name="keyOnCollection">This is the name of the key which will be used to identify collection items</param>
        /// <returns></returns>
        public static object UpdateCollection(this object item, string collectionName, string keyOnCollection, int[] newItemsIds)
        {
            if (newItemsIds == null || !newItemsIds.Any())
            {
                item.ClearCollection(collectionName);
                return item;
            }

            //get currentIds of current items
            var currentIds = new List<int>();
            var currentCollection = item.GetPropertyValue(collectionName);
            var typeOfItemInCollection = item.GetCollectionItemsType(collectionName);
            if (currentCollection.IsCollection())
            {
                foreach (var itemInCurrentCollection in (IEnumerable)currentCollection)
                {
                    var idofItem = (int)itemInCurrentCollection.GetPropertyValue(keyOnCollection);
                    currentIds.Add(idofItem);
                }
            }

            //add new Items
            foreach (var newId in newItemsIds)
            {
                if (currentIds.All(x => x != newId))
                {
                    var newItem = Activator.CreateInstance(typeOfItemInCollection);
                    newItem.UpdateProperty(keyOnCollection, newId);
                    item.AddToCollection(collectionName, newItem);
                }
            }

            //remove items that should no longer be there
            foreach (var currentId in currentIds)
            {
                if (newItemsIds.Any(c => c == currentId)) continue;
                item.RemoveFromCollection(collectionName, keyOnCollection, currentId);
            }

            return item;
        }

        /// <summary>
        /// The updates a collection on an entity by dropping all the items not in the newItemIds list and keeping/adding items in the newItemsIds list
        /// </summary>
        /// <typeparam name="TColType">the type of items in the collection</typeparam>
        /// <param name="item"></param>
        /// <param name="newItemsIdsAsStrings">These are the Ids (as strings) that should remain in the collection </param>
        /// <param name="collectionName">This is the name of the collection eg Material.Categories </param>
        /// <param name="keyOnCollection">This is the name of the key which will be used to identify collection items</param>
        /// <returns></returns>
        public static object UpdateCollection(this object item, string collectionName, string keyOnCollection, string[] newItemsIdsAsStrings)
        {
            var newIds = newItemsIdsAsStrings != null ? newItemsIdsAsStrings.Select(s => Convert.ToInt32(s)).ToArray() : null;
            return item.UpdateCollection(collectionName, keyOnCollection, newIds);
        }
        public static object UpdateCollection(this object item, string collectionName, string keyOnCollection, List<int> newItemsIdsAsStrings)
        {
            var newIds = newItemsIdsAsStrings != null ? newItemsIdsAsStrings.ToArray() : null;
            return item.UpdateCollection(collectionName, keyOnCollection, newIds);
        }

        public static string ToJson(this object item)
        {
            return item.ConvertObjectToJson();
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                return (T)bf.Deserialize(ms);
            }
        }
    }
}
