﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script runat="server">  
  
      void Page_Load(object sender, EventArgs e)  
      {  
  
         if (!IsPostBack)  
         {  
  
            List<TCSOffice.Business.Domain.Entity.Company> companies = null;  
  
            using (TCSOffice.Business.DataAccess.TCSOfficeDbContext TCSOfficeDbContext = new TCSOffice.Business.DataAccess.TCSOfficeDbContext())  
            {  
  
               companies = TCSOfficeDbContext.Companies.Where(z=>z.IsActive == true).ToList();  
  
               ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CompanyReport.rdlc");  
  
  
               ReportDataSource RDS = new ReportDataSource("TCSOfficeDbDataSet", companies);  
  
               ReportViewer1.LocalReport.DataSources.Add(RDS);  
  
               ReportViewer1.LocalReport.Refresh();  
  
            }  
  
         }  
      }  
  
  
   </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
