namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class SwitchConverter
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

		public static void PopulateDayTypeProperties(FTN.DayType cimDayType, ResourceDescription rd)
		{
			if ((cimDayType != null) && (rd != null))
			{
				SwitchConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);
			}
		}

		public static void PopulateSeasonProperties(FTN.Season cimSeason, ResourceDescription rd)
		{
			if ((cimSeason != null) && (rd != null))
			{
				SwitchConverter.PopulateIdentifiedObjectProperties(cimSeason, rd);

				if (cimSeason.EndDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEASON_ENDDATE, cimSeason.EndDate));
				}
				if (cimSeason.StartDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEASON_STARTDATE, cimSeason.StartDate));
				}
			}
		}

		public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegularTimePoint != null) && (rd != null))
			{
				SwitchConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

				if (cimRegularTimePoint.SequenceNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQNUM, cimRegularTimePoint.SequenceNumber));
				}
				if (cimRegularTimePoint.Value1HasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE1, cimRegularTimePoint.Value1));
				}
				if (cimRegularTimePoint.Value2HasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE2, cimRegularTimePoint.Value2));
				}
				if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
					long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
						report.Report.Append("\" - Failed to set reference to RegularTimePoint: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_REGINTSCH, gid));
				}

			}
		}

		public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
		{
			if ((cimBasicIntervalSchedule != null) && (rd != null))
			{
				SwitchConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);
				if (cimBasicIntervalSchedule.StartTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCH_STARTTIME, cimBasicIntervalSchedule.StartTime)); 
				}
				if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCH_VALUE1MUL, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value1Multiplier)));
				}
				if (cimBasicIntervalSchedule.Value1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCH_VALUE1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
				}
				if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCH_VALUE2MUL, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value2Multiplier))); 
				}
				if (cimBasicIntervalSchedule.Value2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCH_VALUE2UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
				}
			}
		}

		public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
		{
			if ((cimRegularIntervalSchedule != null) && (rd != null))
			{
				SwitchConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);

				if (cimRegularIntervalSchedule.EndTimeHasValue)
					rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_ENDTIME, cimRegularIntervalSchedule.EndTime)); 
				if (cimRegularIntervalSchedule.TimeStepHasValue)
					rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP, cimRegularIntervalSchedule.TimeStep));
			}
		}

		public static void PopulateSeasonDayTypeScheduleProperties(FTN.SeasonDayTypeSchedule cimSeasonDayTypeSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeasonDayTypeSchedule != null) && (rd != null))
			{
				SwitchConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd);

				if (cimSeasonDayTypeSchedule.DayTypeHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
						report.Report.Append("\" - Failed to set reference to SeasonDayTypeSchedule: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE, gid)); 
				}

				if (cimSeasonDayTypeSchedule.SeasonHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.Season.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
						report.Report.Append("\" - Failed to set reference to SeasonDayTypeSchedule: rdfID \"").Append(cimSeasonDayTypeSchedule.Season.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SEASONDAYTYPESCHEDULE_SEASON, gid));
				}
			}
		}

		public static void PopulateSwitchScheduleProperties(FTN.SwitchSchedule cimSwitchSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSwitchSchedule != null) && (rd != null))
			{
				SwitchConverter.PopulateSeasonDayTypeScheduleProperties(cimSwitchSchedule, rd, importHelper, report);

				if (cimSwitchSchedule.SwitchHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSwitchSchedule.Switch.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSwitchSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchSchedule.ID);
						report.Report.Append("\" - Failed to set reference to SeasonDayTypeSchedule: rdfID \"").Append(cimSwitchSchedule.Switch.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SWITCHSCHEDULE_SWITCH, gid));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				SwitchConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				SwitchConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				SwitchConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
			}
		}

		public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd)
		{
			if ((cimSwitch != null) && (rd != null))
			{
				SwitchConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);

				if (cimSwitch.NormalOpenHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));
				}
				if (cimSwitch.RetainedHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
				}
				if (cimSwitch.SwitchOnCountHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONCOUNT, cimSwitch.SwitchOnCount));
				} 
				if (cimSwitch.SwitchOnDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));
				}

			}
		}

		public static void PopulateProtectedSwitchProperties(FTN.ProtectedSwitch cimProtectedSwitch, ResourceDescription rd)
		{
			if ((cimProtectedSwitch != null) && (rd != null))
			{
				SwitchConverter.PopulateSwitchProperties(cimProtectedSwitch, rd);
			}
		}

		public static void PopulateBreakerProperties(FTN.Breaker cimBreaker, ResourceDescription rd)
		{
			if ((cimBreaker != null) && (rd != null))
			{
				SwitchConverter.PopulateProtectedSwitchProperties(cimBreaker, rd);
			}
		}

		public static void PopulateRecloserProperties(FTN.Recloser cimRecloser, ResourceDescription rd)
		{
			if ((cimRecloser != null) && (rd != null))
			{
				SwitchConverter.PopulateProtectedSwitchProperties(cimRecloser, rd);
			}
		}

		public static void PopulateLoadBreakSwitchProperties(FTN.LoadBreakSwitch cimLoadBreakSwitch, ResourceDescription rd)
		{
			if ((cimLoadBreakSwitch != null) && (rd != null))
			{
				SwitchConverter.PopulateProtectedSwitchProperties(cimLoadBreakSwitch, rd);
			}
		}
		#endregion Populate ResourceDescription

		#region Enums convert
		public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
		{
			switch (unitMultiplier)
			{
				case FTN.UnitMultiplier.c:
					return UnitMultiplier.c;
				case FTN.UnitMultiplier.d:
					return UnitMultiplier.d;
				case FTN.UnitMultiplier.G:
					return UnitMultiplier.G;
				case FTN.UnitMultiplier.k:
					return UnitMultiplier.k;
				case FTN.UnitMultiplier.m:
					return UnitMultiplier.m;
				case FTN.UnitMultiplier.M:
					return UnitMultiplier.M;
				case FTN.UnitMultiplier.micro:
					return UnitMultiplier.micro;
				case FTN.UnitMultiplier.n:
					return UnitMultiplier.n;
				case FTN.UnitMultiplier.none:
					return UnitMultiplier.none;
				case FTN.UnitMultiplier.p:
					return UnitMultiplier.p;
				case FTN.UnitMultiplier.T:
					return UnitMultiplier.T;
				default:
					return UnitMultiplier.none;
			}
		}

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol symbol)
		{
			switch (symbol)
			{
				case FTN.UnitSymbol.A:
					return UnitSymbol.A;
				case FTN.UnitSymbol.deg:
					return UnitSymbol.deg;
				case FTN.UnitSymbol.degC:
					return UnitSymbol.degC;
				case FTN.UnitSymbol.F:
					return UnitSymbol.F;
				case FTN.UnitSymbol.g:
					return UnitSymbol.g;
				case FTN.UnitSymbol.h:
					return UnitSymbol.h;
				case FTN.UnitSymbol.H:
					return UnitSymbol.H;
				case FTN.UnitSymbol.Hz:
					return UnitSymbol.Hz;
				case FTN.UnitSymbol.J:
					return UnitSymbol.J;
				case FTN.UnitSymbol.m:
					return UnitSymbol.m;
				case FTN.UnitSymbol.m2:
					return UnitSymbol.m2;
				case FTN.UnitSymbol.m3:
					return UnitSymbol.m3;
				case FTN.UnitSymbol.min:
					return UnitSymbol.min;
				case FTN.UnitSymbol.N:
					return UnitSymbol.N;
				case FTN.UnitSymbol.none:
					return UnitSymbol.none;
				case FTN.UnitSymbol.ohm:
					return UnitSymbol.ohm;
				case FTN.UnitSymbol.Pa:
					return UnitSymbol.Pa;
				case FTN.UnitSymbol.rad:
					return UnitSymbol.rad;
				case FTN.UnitSymbol.s:
					return UnitSymbol.s;
				case FTN.UnitSymbol.S:
					return UnitSymbol.S;
				case FTN.UnitSymbol.V:
					return UnitSymbol.V;
				case FTN.UnitSymbol.VA:
					return UnitSymbol.VA;
				case FTN.UnitSymbol.VAh:
					return UnitSymbol.VAh;
				case FTN.UnitSymbol.VAr:
					return UnitSymbol.VAr;
				case FTN.UnitSymbol.VArh:
					return UnitSymbol.VArh;
				case FTN.UnitSymbol.W:
					return UnitSymbol.W;
				case FTN.UnitSymbol.Wh:
					return UnitSymbol.Wh;
				default:
					return UnitSymbol.none;
			}
		}
		#endregion Enums convert
	}
}
