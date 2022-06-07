using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Conductor : ConductingEquipment
    {
        public float length;

        public Conductor(long globalID) : base(globalID)
        {
        }

		public float Length
		{
			get
			{
				return length;
			}

			set
			{
				length = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Conductor x = (Conductor)obj;
				return ((x.Length == this.Length));			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.CONDUCTOR_LENGTH:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.CONDUCTOR_LENGTH:
					prop.SetValue(length);
					break;
				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CONDUCTOR_LENGTH:
					length = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
