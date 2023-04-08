using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class RegularIntervalSchedule : BasicIntervalSchedule
	{
		private List<long> regularTimePoints = new List<long>();
		private DateTime endTime;
		private float timeStep;
		public RegularIntervalSchedule(long globalId) : base(globalId)
		{
		}

		public List<long> RegularTimePoints
		{
			get
			{
				return regularTimePoints;
			}

			set
			{
				regularTimePoints = value;
			}
		}

		public DateTime EndTime { get => endTime; set => endTime = value; }
		public float TimeStep { get => timeStep; set => timeStep = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				RegularIntervalSchedule x = (RegularIntervalSchedule)obj;
				return (x.endTime == this.endTime &&
						x.timeStep == this.timeStep &&
						CompareHelper.CompareLists(x.regularTimePoints, this.regularTimePoints, true));
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
				case ModelCode.REGULARINTERVALSCHEDULE_REGTIMEPS:
				case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
				case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.REGULARINTERVALSCHEDULE_REGTIMEPS:
					prop.SetValue(regularTimePoints);
					break;
				case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
					prop.SetValue(endTime);
					break;
				case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
					prop.SetValue(timeStep);
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
				case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
					endTime = property.AsDateTime();
					break;
				case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
					timeStep = property.AsFloat();
					break;
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
				return regularTimePoints.Count > 0 || base.IsReferenced;
			}
		}


		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regularTimePoints != null && regularTimePoints.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.REGULARINTERVALSCHEDULE_REGTIMEPS] = regularTimePoints.GetRange(0, regularTimePoints.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.REGULARTIMEPOINT_REGINTSCH:
					regularTimePoints.Add(globalId);
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
				case ModelCode.REGULARTIMEPOINT_REGINTSCH:

					if (regularTimePoints.Contains(globalId))
					{
						regularTimePoints.Remove(globalId);
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
