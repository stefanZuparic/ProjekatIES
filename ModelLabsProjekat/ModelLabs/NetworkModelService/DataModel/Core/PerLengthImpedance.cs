using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class PerLengthImpedance : IdentifiedObject
    {
        private List<long> acLineSegments = new List<long>();

        public PerLengthImpedance(long globalID) : base(globalID)
        {
        }

		public List<long> AcLineSegments
		{
			get
			{
				return acLineSegments;
			}

			set
			{
				acLineSegments = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PerLengthImpedance x = (PerLengthImpedance)obj;
				return (CompareHelper.CompareLists(x.acLineSegments, this.acLineSegments));
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
				case ModelCode.PERLENGTHIMPEDANCE_ACLINESEGMENTS:
					return true;
				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.PERLENGTHIMPEDANCE_ACLINESEGMENTS:
					prop.SetValue(acLineSegments);
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
				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return acLineSegments.Count > 0 || base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (acLineSegments != null && acLineSegments.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.PERLENGTHIMPEDANCE_ACLINESEGMENTS] = acLineSegments.GetRange(0, acLineSegments.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE:
					acLineSegments.Add(globalId);
					break;

				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE:

					if (acLineSegments.Contains(globalId))
					{
						acLineSegments.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;

				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}

		#endregion IReference implementation
	}
}
