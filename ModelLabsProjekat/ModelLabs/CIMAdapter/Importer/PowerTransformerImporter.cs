﻿using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void  ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			///
			ImportPerLengthSequenceImpedance();
			ImportSeriesCompensator();
			ImportConnectivityNode();
			ImportDcLineSegment();
			ImportAcLineSegment();
			ImportTerminal();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import

		private void ImportPerLengthSequenceImpedance() {
			SortedDictionary<string, object> cimPerLengthSequenceImpedances = concreteModel.GetAllObjectsOfType("FTN.PerLengthSequenceImpedance");
			if (cimPerLengthSequenceImpedances != null)
			{
				foreach (KeyValuePair<string, object> cimPerLengthSequenceImpedancePair in cimPerLengthSequenceImpedances)
				{
					FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance = cimPerLengthSequenceImpedancePair.Value as FTN.PerLengthSequenceImpedance;

					ResourceDescription rd = CreatePerLengthSequenceImpedanceDescription(cimPerLengthSequenceImpedance);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLengthSequenceImpedance.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLengthSequenceImpedance.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportSeriesCompensator()
		{
			SortedDictionary<string, object> cimSeriesCompensators = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");
			if (cimSeriesCompensators != null)
			{
				foreach (KeyValuePair<string, object> cimSeriesCompensatorPair in cimSeriesCompensators)
				{
					FTN.SeriesCompensator cimSeriesCompensator = cimSeriesCompensatorPair.Value as FTN.SeriesCompensator;

					ResourceDescription rd = CreateSeriesCompensatorDescription(cimSeriesCompensator);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportConnectivityNode() 
		{
			SortedDictionary<string, object> cimConnectivityNodes = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNode");
			if (cimConnectivityNodes != null)
			{
				foreach (KeyValuePair<string, object> cimConnectivityNodePair in cimConnectivityNodes)
				{
					FTN.ConnectivityNode cimConnectivityNode = cimConnectivityNodePair.Value as FTN.ConnectivityNode;

					ResourceDescription rd = CreateConnectivityNodeDescription(cimConnectivityNode);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportDcLineSegment()
		{
			SortedDictionary<string, object> cimDcLineSegments = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");
			if (cimDcLineSegments != null)
			{
				foreach (KeyValuePair<string, object> cimDcLineSegmentPair in cimDcLineSegments)
				{
					FTN.DCLineSegment cimDcLineSegment = cimDcLineSegmentPair.Value as FTN.DCLineSegment;

					ResourceDescription rd = CreateDcLineSegmentDescription(cimDcLineSegment);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("DcLineSegment ID = ").Append(cimDcLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("DcLineSegment ID = ").Append(cimDcLineSegment.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportAcLineSegment()
		{
			SortedDictionary<string, object> cimAcLineSegments = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");
			if (cimAcLineSegments != null)
			{
				foreach (KeyValuePair<string, object> cimAcLineSegmentPair in cimAcLineSegments)
				{
					FTN.ACLineSegment cimAcLineSegment = cimAcLineSegmentPair.Value as FTN.ACLineSegment;

					ResourceDescription rd = CreateAcLineSegmentDescription(cimAcLineSegment);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AcLineSegment ID = ").Append(cimAcLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AcLineSegment ID = ").Append(cimAcLineSegment.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportTerminal()
		{
			SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
			if (cimTerminals != null)
			{
				foreach (KeyValuePair<string, object> cimTerminalPair in cimTerminals)
				{
					FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

					ResourceDescription rd = CreateTerminalDescription(cimTerminal);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

        #endregion Import

        #region Create
        private ResourceDescription CreatePerLengthSequenceImpedanceDescription(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance) 
		{
			ResourceDescription rd = null;
			if (cimPerLengthSequenceImpedance != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PERLENGTHSEQUENCEIMPEDANCE, importHelper.CheckOutIndexForDMSType(DMSType.PERLENGTHSEQUENCEIMPEDANCE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimPerLengthSequenceImpedance.ID, gid);

				PowerTransformerConverter.PopulatePerLengthSequenceImpedanceProperties(cimPerLengthSequenceImpedance, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateSeriesCompensatorDescription(FTN.SeriesCompensator cimSeriesCompensator)
		{
			ResourceDescription rd = null;
			if (cimSeriesCompensator != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SERIESCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.SERIESCOMPENSATOR));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSeriesCompensator.ID, gid);

				PowerTransformerConverter.PopulateSeriesCompensatorProperties(cimSeriesCompensator, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateConnectivityNodeDescription(FTN.ConnectivityNode cimConnectivityNode)
		{
			ResourceDescription rd = null;
			if (cimConnectivityNode != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONNECTIVITYNODE, importHelper.CheckOutIndexForDMSType(DMSType.CONNECTIVITYNODE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimConnectivityNode.ID, gid);

				PowerTransformerConverter.PopulateConnectivityNodeProperties(cimConnectivityNode, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateDcLineSegmentDescription(FTN.DCLineSegment cimDcLineSegment)
		{
			ResourceDescription rd = null;
			if (cimDcLineSegment != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DCLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.DCLINESEGMENT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimDcLineSegment.ID, gid);

				PowerTransformerConverter.PopulateDCLineSegmentProperties(cimDcLineSegment, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateAcLineSegmentDescription(FTN.ACLineSegment cimAcLineSegment)
		{
			ResourceDescription rd = null;
			if (cimAcLineSegment != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ACLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.ACLINESEGMENT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimAcLineSegment.ID, gid);

				PowerTransformerConverter.PopulateACLineSegmentProperties(cimAcLineSegment, rd, importHelper, report);
			}
			return rd;
		}
		
		private ResourceDescription CreateTerminalDescription(FTN.Terminal cimTerminal)
		{
			ResourceDescription rd = null;
			if (cimTerminal != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTerminal.ID, gid);

				PowerTransformerConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
			}
			return rd;
		}
        #endregion Create
    }
}

