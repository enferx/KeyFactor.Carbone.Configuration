using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Shared;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KeyFactor.Carbone.Configuration.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class ConfigurationPageModel: AbpPageModel
    {
        protected string EntityPath { get; private set; }

        protected Dictionary<string, string> Settings { get; private set; }

        protected ConfigurationPageModel(string entityPath = "")
        {
            LocalizationResourceType = typeof(ConfigurationResource);
            ObjectMapperContext = typeof(ConfigurationWebModule);
            EntityPath = entityPath;
        }

        public void SetEntityPath(string entityPath)
        {
            EntityPath = entityPath;
        }

        protected virtual void ConfigureOnGet()
        {

        }
    }
}