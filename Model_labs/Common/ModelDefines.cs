using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		REGULARTIMEPOINT					= 0x0001,
		DAYTYPE								= 0x0002,
		SEASON								= 0x0003,
		SWITCHSCHEDULE						= 0x0004,
		BREAKER								= 0x0005,
		RECLOSER							= 0x0006,
		LOADBREAKSWITCH						= 0x0007,


		/*BASEVOLTAGE						= 0x0001*/
	}

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ								= 0x1000000000000000,
		IDOBJ_GID							= 0x1000000000000104,
		IDOBJ_ALIASNAME						= 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,

		POWERSYSTEMRESOURCE				    = 0x1100000000000000,

		BASICINTERVALSCH					= 0x1200000000000000,
		BASICINTERVALSCH_STARTTIME			= 0x1200000000000108,
		BASICINTERVALSCH_VALUE1MUL			= 0x120000000000020a,
		BASICINTERVALSCH_VALUE1UNIT			= 0x120000000000030a,
		BASICINTERVALSCH_VALUE2MUL			= 0x120000000000040a,
		BASICINTERVALSCH_VALUE2UNIT			= 0x120000000000050a,

		REGULARTIMEPOINT					= 0x1300000000010000,
		REGULARTIMEPOINT_SEQNUM				= 0x1300000000010103,
		REGULARTIMEPOINT_VALUE1				= 0x1300000000010205,
		REGULARTIMEPOINT_VALUE2				= 0x1300000000010305,
		REGULARTIMEPOINT_REGINTSCH			= 0x1300000000010409,

		DAYTYPE								= 0x1400000000020000,
		DAYTYPE_SEASONDAYTYPESCHS			= 0x1400000000020119,

		SEASON								= 0x1500000000030000,
		SEASON_ENDDATE						= 0x1500000000030108,
		SEASON_STARTDATE					= 0x1500000000030208,
		SEASON_SEASONDAYTYPESCHS			= 0X1500000000030319,

		EQUIPMENT							= 0x1110000000000000,
		EQUIPMENT_AGGREGATE					= 0x1110000000000101,
		EQUIPMENT_NORMALLYINSERVICE			= 0x1110000000000201,

		REGULARINTERVALSCHEDULE				= 0x1210000000000000,
		REGULARINTERVALSCHEDULE_REGTIMEPS	= 0x1210000000000119,
		REGULARINTERVALSCHEDULE_ENDTIME		= 0x1210000000000208,
		REGULARINTERVALSCHEDULE_TIMESTEP    = 0x1210000000000305,


		CONDUCTINGEQUIPMENT					= 0x1111000000000000,

		SEASONDAYTYPESCHEDULE				= 0x1211000000000000,
		SEASONDAYTYPESCHEDULE_DAYTYPE		= 0x1211000000000109,
		SEASONDAYTYPESCHEDULE_SEASON		= 0x1211000000000209,

		SWITCH								= 0x1111100000000000,
		SWITCH_NORMALOPEN					= 0x1111100000000101,
		SWITCH_RETAINED						= 0x1111100000000201,
		SWITCH_SWITCHONCOUNT				= 0x1111100000000303,
		SWITCH_SWITCHONDATE					= 0x1111100000000408,
		SWITCH_SWITCHSCHEDULES				= 0x1111100000000519,

		SWITCHSCHEDULE						= 0x1211100000040000,
		SWITCHSCHEDULE_SWITCH				= 0x1211100000040109,

		PROTECTEDSWITCH						= 0x1111110000000000,

		BREAKER								= 0x1111111000050000,

		RECLOSER							= 0x1111112000060000,

		LOADBREAKSWITCH						= 0x1111113000070000,




		/*IDOBJ_DESCRIPTION					= 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,	

		PSR									= 0x1100000000000000,
		PSR_CUSTOMTYPE						= 0x1100000000000107,
		PSR_LOCATION						= 0x1100000000000209,

		BASEVOLTAGE							= 0x1200000000010000,
		BASEVOLTAGE_NOMINALVOLTAGE			= 0x1200000000010105,
		BASEVOLTAGE_CONDEQS					= 0x1200000000010219,

		LOCATION							= 0x1300000000020000,
		LOCATION_CORPORATECODE				= 0x1300000000020107,
		LOCATION_CATEGORY					= 0x1300000000020207,
		LOCATION_PSRS						= 0x1300000000020319,

		WINDINGTEST							= 0x1400000000050000,
		WINDINGTEST_LEAKIMPDN				= 0x1400000000050105,
		WINDINGTEST_LOADLOSS				= 0x1400000000050205,
		WINDINGTEST_NOLOADLOSS				= 0x1400000000050305,
		WINDINGTEST_PHASESHIFT				= 0x1400000000050405,
		WINDINGTEST_LEAKIMPDN0PERCENT		= 0x1400000000050505,
		WINDINGTEST_LEAKIMPDNMINPERCENT		= 0x1400000000050605,
		WINDINGTEST_LEAKIMPDNMAXPERCENT		= 0x1400000000050705,
		WINDINGTEST_POWERTRWINDING			= 0x1400000000050809,

		EQUIPMENT							= 0x1110000000000000,
		EQUIPMENT_ISUNDERGROUND				= 0x1110000000000101,
		EQUIPMENT_ISPRIVATE					= 0x1110000000000201,		

		CONDEQ								= 0x1111000000000000,
		CONDEQ_PHASES						= 0x111100000000010a,
		CONDEQ_RATEDVOLTAGE					= 0x1111000000000205,
		CONDEQ_BASVOLTAGE					= 0x1111000000000309,

		POWERTR								= 0x1112000000030000,
		POWERTR_FUNC						= 0x111200000003010a,
		POWERTR_AUTO						= 0x1112000000030201,
		POWERTR_WINDINGS					= 0x1112000000030319,

		POWERTRWINDING						= 0x1111100000040000,
		POWERTRWINDING_CONNTYPE				= 0x111110000004010a,
		POWERTRWINDING_GROUNDED				= 0x1111100000040201,
		POWERTRWINDING_RATEDS				= 0x1111100000040305,
		POWERTRWINDING_RATEDU				= 0x1111100000040405,
		POWERTRWINDING_WINDTYPE				= 0x111110000004050a,
		POWERTRWINDING_PHASETOGRNDVOLTAGE	= 0x1111100000040605,
		POWERTRWINDING_PHASETOPHASEVOLTAGE	= 0x1111100000040705,
		POWERTRWINDING_POWERTRW				= 0x1111100000040809,
		POWERTRWINDING_TESTS				= 0x1111100000040919,*/
	}

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


