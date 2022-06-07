using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private long connectivityNode;
        private long conductingEquipment;

		public Terminal(long globalID) : base(globalID)
		{
		}

		public long ConnectivityNode
		{
			get
			{
				return connectivityNode;
			}

			set
			{
				connectivityNode = value;
			}
		}

		public long ConductingEquipment
		{
			get
			{
				return conductingEquipment;
			}

			set
			{
				conductingEquipment = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Terminal x = (Terminal)obj;
				return ((x.ConductingEquipment == this.ConductingEquipment) &&
						(x.ConnectivityNode == this.ConnectivityNode));
			}
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
				case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
				case ModelCode.TERMINAL_CONNECTIVITYNODE:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
					prop.SetValue(conductingEquipment);
					break;
				case ModelCode.TERMINAL_CONNECTIVITYNODE:
					prop.SetValue(connectivityNode);
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
				case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
					conductingEquipment = property.AsReference();
					break;

				case ModelCode.TERMINAL_CONNECTIVITYNODE:
					connectivityNode = property.AsReference();
					break;
				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (connectivityNode != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.TERMINAL_CONNECTIVITYNODE] = new List<long>();
				references[ModelCode.TERMINAL_CONNECTIVITYNODE].Add(connectivityNode);
			}

			if (conductingEquipment != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.TERMINAL_CONDUCTINGEQUIPMENT] = new List<long>();
				references[ModelCode.TERMINAL_CONDUCTINGEQUIPMENT].Add(conductingEquipment);
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation

	}
}
