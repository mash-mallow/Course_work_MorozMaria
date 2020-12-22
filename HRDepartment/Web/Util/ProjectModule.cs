﻿using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Util
{
    public class ProjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();
        }
    }
}