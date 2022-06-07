using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class ACLineSegment : Conductor
    {
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;
        private long perLengthImpedance;

        public ACLineSegment(long globalID) : base(globalID)
        {
        }

        public float B0ch { get => b0ch; set => b0ch = value; }
        public float Bch { get => bch; set => bch = value; }
        public float G0ch { get => g0ch; set => g0ch = value; }
        public float Gch { get => gch; set => gch = value; }
        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }
        public long PerLengthImpedance { get => perLengthImpedance; set => perLengthImpedance = value; }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ACLineSegment x = (ACLineSegment)obj;
                return ((x.B0ch == this.B0ch) &&
                        (x.Bch == this.Bch) &&
                        (x.G0ch == this.G0ch) &&
                        (x.Gch == this.Gch) &&
                        (x.R == this.R) &&
                        (x.R0 == this.R0) &&
                        (x.X == this.X) &&
                        (x.X0 == this.X0) && 
                        (x.PerLengthImpedance == this.PerLengthImpedance)
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
                case ModelCode.ACLINESEGMENT_B0CH:
                case ModelCode.ACLINESEGMENT_BCH:
                case ModelCode.ACLINESEGMENT_G0CH:
                case ModelCode.ACLINESEGMENT_GCH:
                case ModelCode.ACLINESEGMENT_R:
                case ModelCode.ACLINESEGMENT_R0:
                case ModelCode.ACLINESEGMENT_X:
                case ModelCode.ACLINESEGMENT_X0:
                case ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.ACLINESEGMENT_B0CH:
                    prop.SetValue(b0ch);
                    break;
                case ModelCode.ACLINESEGMENT_BCH:
                    prop.SetValue(bch);
                    break;
                case ModelCode.ACLINESEGMENT_G0CH:
                    prop.SetValue(g0ch);
                    break;
                case ModelCode.ACLINESEGMENT_GCH:
                    prop.SetValue(gch);
                    break;
                case ModelCode.ACLINESEGMENT_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.ACLINESEGMENT_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.ACLINESEGMENT_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.ACLINESEGMENT_X0:
                    prop.SetValue(x0);
                    break;
                case ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE:
                    prop.SetValue(perLengthImpedance);
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
                case ModelCode.ACLINESEGMENT_B0CH:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_BCH:
                    bch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_G0CH:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_GCH:
                    gch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_X0:
                    x0 = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE:
                    perLengthImpedance = property.AsReference();
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
            if (perLengthImpedance != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE] = new List<long>();
                references[ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE].Add(perLengthImpedance);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
