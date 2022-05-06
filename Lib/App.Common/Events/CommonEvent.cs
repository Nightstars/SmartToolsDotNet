using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Common.Models.events;
using Prism.Events;

namespace App.Common.Events
{
    public class CommonEvent : PubSubEvent<CommoneventInfo<object>>
    {
    }
}
