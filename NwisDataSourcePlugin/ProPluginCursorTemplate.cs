﻿using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class ProPluginCursorTemplate : PluginCursorTemplate
    {
        public override PluginRow GetCurrentRow()
        {
            var listOfRowValues = new List<object>();
            //TODO collect the values for the current row

            return new PluginRow(listOfRowValues);
        }

        public override bool MoveNext()
        {
            //TODO determine if there are more rows
            throw new NotImplementedException();
        }
    }
}
