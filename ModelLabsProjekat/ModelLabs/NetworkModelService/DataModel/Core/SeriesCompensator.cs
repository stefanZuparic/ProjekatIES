using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class SeriesCompensator : ConductingEquipment
    {
        private float r;
        private float r0;
        private float x;
        private float x0;

        public SeriesCompensator(long globalID) : base(globalID)
        {
        }

        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeriesCompensator x = (SeriesCompensator)obj;
                return ((x.R == this.R) &&
                        (x.R0 == this.R0) &&
                        (x.X == this.X) &&
                        (x.X0 == this.X0)
                        );
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
                case ModelCode.SERIESCOMPENSATOR_R:
                case ModelCode.SERIESCOMPENSATOR_R0:
                case ModelCode.SERIESCOMPENSATOR_X:
                case ModelCode.SERIESCOMPENSATOR_X0:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SERIESCOMPENSATOR_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.SERIESCOMPENSATOR_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.SERIESCOMPENSATOR_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.SERIESCOMPENSATOR_X0:
                    prop.SetValue(x0);
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
                case ModelCode.SERIESCOMPENSATOR_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_X0:
                    x0 = property.AsFloat();
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
            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
