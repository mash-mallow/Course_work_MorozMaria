﻿using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Util
{
    public class DepartmentModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDepartmentService>().To<DepartmentService>();
        }
    }
}