using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.DataBase
{
    public partial class Passenger
    {
        public void initialize(SkySharkDbContainer context, string id, string name)
        {
			if (id == null)
				throw new ArgumentNullException("id is null");
            ID = id;
			if (name == null)
				throw new ArgumentNullException("name is null");
            Name = name;
			if (context == null)
				throw new ArgumentNullException("context is null");
            context.PassengerSet.Add(this);
        }
    }
}