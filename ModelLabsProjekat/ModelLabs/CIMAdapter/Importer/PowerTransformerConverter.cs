namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulatePerLengthImpedanceProperties(FTN.PerLengthImpedance cimPerLengthImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPerLengthImpedance != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPerLengthImpedance, rd);
			}
		}

		public static void PopulatePerLengthSequenceImpedanceProperties(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPerLengthSequenceImpedance != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePerLengthImpedanceProperties(cimPerLengthSequenceImpedance, rd, importHelper, report);

				if (cimPerLengthSequenceImpedance.B0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_B0CH, cimPerLengthSequenceImpedance.B0ch));
				}
				if (cimPerLengthSequenceImpedance.BchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_BCH, cimPerLengthSequenceImpedance.Bch));
				}
				if (cimPerLengthSequenceImpedance.G0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_G0CH, cimPerLengthSequenceImpedance.G0ch));
				}
				if (cimPerLengthSequenceImpedance.GchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_GCH, cimPerLengthSequenceImpedance.Gch));
				}
				if (cimPerLengthSequenceImpedance.RHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R, cimPerLengthSequenceImpedance.R));
				}
				if (cimPerLengthSequenceImpedance.R0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_R0, cimPerLengthSequenceImpedance.R0));
				}
				if (cimPerLengthSequenceImpedance.XHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X, cimPerLengthSequenceImpedance.X));
				}
				if (cimPerLengthSequenceImpedance.X0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.PERLENGTHSEQUENCEIMPEDANCE_X0, cimPerLengthSequenceImpedance.X0));
				}
			}
		}

		public static void PopulateConnectivityNodeProperties(FTN.ConnectivityNode cimConnectivityNode, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConnectivityNode != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);

				if (cimConnectivityNode.DescriptionHasValue) {
					rd.AddProperty(new Property(ModelCode.CONNECTIVITYNODE_DESCRIPTION, cimConnectivityNode.Description));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);
			}
		}

		public static void PopulateSeriesCompensatorProperties(FTN.SeriesCompensator cimSeriesCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeriesCompensator != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSeriesCompensator, rd, importHelper, report);
				if (cimSeriesCompensator.RHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_R, cimSeriesCompensator.R));
				}
				if (cimSeriesCompensator.R0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_R0, cimSeriesCompensator.R0));
				}
				if (cimSeriesCompensator.XHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_X, cimSeriesCompensator.X));
				}
				if (cimSeriesCompensator.X0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_X0, cimSeriesCompensator.X0));
				}
			}
		}

		public static void PopulateConductorProperties(FTN.Conductor cimConductor, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductor != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimConductor, rd, importHelper, report);
				if (cimConductor.LengthHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDUCTOR_LENGTH, cimConductor.Length));
				}
			}
		}

		public static void PopulateDCLineSegmentProperties(FTN.DCLineSegment cimDCLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimDCLineSegment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductorProperties(cimDCLineSegment, rd, importHelper, report);
			}
		}

		public static void PopulateACLineSegmentProperties(FTN.ACLineSegment cimACLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimACLineSegment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductorProperties(cimACLineSegment, rd, importHelper, report);
			}
			if (cimACLineSegment.B0chHasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_B0CH, cimACLineSegment.B0ch));
			}
			if (cimACLineSegment.BchHasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_BCH, cimACLineSegment.Bch));
			}
			if (cimACLineSegment.G0chHasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_G0CH, cimACLineSegment.G0ch));
			}
			if (cimACLineSegment.GchHasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_GCH, cimACLineSegment.Gch));
			}
			if (cimACLineSegment.RHasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_R, cimACLineSegment.R));
			}
			if (cimACLineSegment.R0HasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_R0, cimACLineSegment.R0));
			}
			if (cimACLineSegment.XHasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_X, cimACLineSegment.X));
			}
			if (cimACLineSegment.X0HasValue)
			{
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_X0, cimACLineSegment.X0));
			}

			//referenca 
			if (cimACLineSegment.PerLengthImpedanceHasValue)
			{
				long gid = importHelper.GetMappedGID(cimACLineSegment.PerLengthImpedance.ID);
				if (gid < 0)
				{
					report.Report.Append("WARNING: Convert ").Append(cimACLineSegment.GetType().ToString()).Append(" rdfID = \"").Append(cimACLineSegment.ID);
					report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimACLineSegment.PerLengthImpedance.ID).AppendLine(" \" is not mapped to GID!");
				}
				rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_PERLENGTHIMPEDANCE, gid));
			}
		}

		public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTerminal != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);
			}

			//referenca 
			if (cimTerminal.ConnectivityNodeHasValue)
			{
				long gid = importHelper.GetMappedGID(cimTerminal.ConnectivityNode.ID);
				if (gid < 0)
				{
					report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
					report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
				}
				rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTIVITYNODE, gid));
			}

			if (cimTerminal.ConductingEquipmentHasValue)
			{
				long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
				if (gid < 0)
				{
					report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
					report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
				}
				rd.AddProperty(new Property(ModelCode.TERMINAL_CONDUCTINGEQUIPMENT, gid));
			}
		}

		#endregion Populate ResourceDescription

		#region Enums convert
		#endregion Enums convert
	}
}
