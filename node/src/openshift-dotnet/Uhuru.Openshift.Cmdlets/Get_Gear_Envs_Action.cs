﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Uhuru.Openshift.Runtime;
using Uhuru.Openshift.Runtime.Utils;

namespace Uhuru.Openshift.Cmdlets
{
    [Cmdlet("Get", "Gear-Envs-Action")]
    public class Get_Gear_Envs_Action : System.Management.Automation.Cmdlet 
    {
        [Parameter]
        public string Uuid;

        protected override void ProcessRecord()
        {
            ReturnStatus returnStatus = new ReturnStatus();

            try
            {
                Logger.Debug(string.Format("Running Get-Gear-Envs-Action for {0}", Uuid));
                string containerDir = ApplicationContainer.GetFromUuid(Uuid).ContainerDir;
                Dictionary<string,string> envVars = Environ.ForGear(containerDir);
                returnStatus.Output = JsonConvert.SerializeObject(envVars);
                Logger.Debug(string.Format("Output for Get-Gear-Envs-Action{0} ", returnStatus.Output));
                returnStatus.ExitCode = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                returnStatus.ExitCode = 1;
            }
            
            this.WriteObject(returnStatus);

        }
        
    }
}