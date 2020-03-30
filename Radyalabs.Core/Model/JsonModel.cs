using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Model
{
    public class JsonModel
    {
        [JsonProperty(PropertyName = "error")]
        public bool Error { get; set; }

        [JsonProperty(PropertyName = "force")]
        public bool ForceLogout { get; set; }

        [JsonProperty(PropertyName = "alert")]
        public JsonAlertModel Alerts { get; set; }

        [JsonProperty(PropertyName = "data")]
        public Object Data { get; set; }

        public JsonModel()
        {
            this.Alerts = new JsonAlertModel();
            this.Data = new Object();
        }

        public void SetError(bool error)
        {
            this.Error = error;
        }

        public bool GetError()
        {
            return this.Error;
        }

        public void AddAlert(int code, string message)
        {
            this.Alerts.Code = code;
            this.Alerts.Message = message;
        }

        public JsonAlertModel GetAlert()
        {
            return this.Alerts;
        }

        public void AddData(Object data)
        {
            this.Data = data;
        }

        public Object GetData()
        {
            return this.Data;
        }

        public void SetForceLogout(bool forceLogout)
        {
            this.ForceLogout = forceLogout;
        }

        public bool GetForceLogout()
        {
            return this.ForceLogout;
        }

        public void SetJsonProperty(bool error, bool forceLogout, int code, string message, object data = null)
        {
            this.Error = error;
            this.ForceLogout = forceLogout;
            this.Alerts.Code = code;
            this.Alerts.Message = message;
            this.Data = data;
        }
    }
}
