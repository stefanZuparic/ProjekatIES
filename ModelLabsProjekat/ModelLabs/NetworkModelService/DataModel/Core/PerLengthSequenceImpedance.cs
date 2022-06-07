using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class PerLengthSequenceImpedance : PerLengthImpedance
    {
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;

        public PerLengthSequenceImpedance(long globalID) : base(globalID)
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

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PerLengthSequenceImpedance x = (PerLengthSequenceImpedance)obj;
                return ((x.B0ch == this.B0ch) &&
                        (x.Bch == this.Bch) &&
                        (x.G0ch == this.G0ch) &&
                        (x.Gch == this.Gch) &&
                        (x.R == this.R) &&
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
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_B0CH:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_BCH:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_G0CH:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_GCH:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R0:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X:
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X0:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_B0CH:
                    prop.SetValue(b0ch);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_BCH:
                    prop.SetValue(bch);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_G0CH:
                    prop.SetValue(g0ch);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_GCH:
                    prop.SetValue(gch);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X0:
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
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_B0CH:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_BCH:
                    bch = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_G0CH:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_GCH:
                    gch = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X0:
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
