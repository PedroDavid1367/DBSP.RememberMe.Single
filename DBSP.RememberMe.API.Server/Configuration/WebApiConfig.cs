using DBSP.RememberMe.API.Model;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace DBSP.RememberMe.API.Server.Configuration
{
  public static class WebApiConfig
  {
    public static HttpConfiguration Register()
    {
      var config = new HttpConfiguration();

      config.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel());

      var cors = new EnableCorsAttribute("http://localhost:8080 , http://localhost:8888", "*", "*");
      config.EnableCors(cors);
      config.EnsureInitialized();

      return config;
    }

    private static IEdmModel GetEdmModel()
    {
      var builder = new ODataConventionModelBuilder();
      builder.Namespace = "RememberMe";
      builder.ContainerName = "RememberMeContainer";

      builder.EntitySet<Note>("Notes");

      return builder.GetEdmModel();
    }
  }
}