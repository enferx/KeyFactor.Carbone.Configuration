using KeyFactor.Carbone.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace KeyFactor.Carbone.Configuration.Products
{
    [RemoteService(isEnabled: false, IsMetadataEnabled = false)]
    [Authorize(ConfigurationPermissions.Products.Default)]

    public class ProductFamilyAppService : ConfigurationAppService
    {
    }
}
