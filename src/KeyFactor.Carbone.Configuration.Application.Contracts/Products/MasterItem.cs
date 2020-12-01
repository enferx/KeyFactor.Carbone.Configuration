using System;
using System.Collections.Generic;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class MasterItem
    {
        public string Name { get; set; }

        public List<ChildItem> Items { get; set; }
    }
}
