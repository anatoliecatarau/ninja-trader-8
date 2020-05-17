#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class TradingDJInsideBarParameter : Indicator
	{
		#region Variables
		//Will keep the number of rising bars
		private int risingBarsCount;
		//Will keep the number of falling bars
		private int fallingBarsCount;
		#endregion
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"A simple Inside Bar Indicator preceded by N bars that make consecutive lower lows and lower highs or consecutive higher lows and higher highs. We will use the parameter MinimumConsecutiveBars to keep that value.";
				Name										= "TradingDJInsideBarParameter";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= false;
				DrawOnPricePanel							= false;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= false;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				MinimumConsecutiveBars					= 3;
				RisingBarsBrush							= Brushes.Orange;
				FallingBarsBrush						= Brushes.Blue;
			}
			else if (State == State.Configure)
			{
				risingBarsCount = 0;
				fallingBarsCount = 0;
			}
		}

		protected override void OnBarUpdate()
		{
			// Since the calculations require that there be at least one previous bar
			// we'll need to check for that and exit if this is the first bar.
			if (CurrentBar < 1)
			{
				return;
			}
			
			checkForInsideBar();
			manageRisingBars();
			manageFallingBars();
		}
		
		private void checkForInsideBar()
		{
			if(risingBarsCount >= MinimumConsecutiveBars
				&& High[0] <= High[1] && Low[0] >= Low[1])
			{
				//Here goes the logic for the pattern Inside Bar and minimumConsecutiveBars rising bars
				//We will simply set the color of the inside bar orange
				BarBrush = RisingBarsBrush;
			}
			
			if(fallingBarsCount >= MinimumConsecutiveBars
				&& High[0] <= High[1] && Low[0] >= Low[1])
			{
				//Here goes the logic for the pattern Inside Bar and minimumConsecutiveBars falling bars
				//We will simply set the color of the inside bar blue
				BarBrush = FallingBarsBrush;
			}
		}
		
		private void manageRisingBars()
		{
			if(High[0] > High[1] && Low[0] > Low[1])
			{
				risingBarsCount++;
			}
			else
			{
				risingBarsCount = 0;
			}
		}
		
		private void manageFallingBars()
		{
			if(High[0] < High[1] && Low[0] < Low[1])
			{
				fallingBarsCount++;
			}
			else
			{
				fallingBarsCount=0;
			}
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(0, int.MaxValue)]
		[Display(Name="MinimumConsecutiveBars", Description="The number of minimum consecutive rising or falling bars preceding the inside bar", Order=1, GroupName="Parameters")]
		public int MinimumConsecutiveBars
		{ get; set; }
		
		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="RisingBarsBrush", Description="Brush for the inside bar following consecutive rising bars", Order=1, GroupName="Parameters")]
		public Brush RisingBarsBrush
		{ get; set; }
		[Browsable(false)]
		public string RisingBarsBrushSerializable
		{
			get { return Serialize.BrushToString(RisingBarsBrush); }
			set { RisingBarsBrush = Serialize.StringToBrush(value); }
		}			

		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="FallingBarsBrush", Description="Brush for the inside bar following consecutive falling bars", Order=2, GroupName="Parameters")]
		public Brush FallingBarsBrush
		{ get; set; }

		[Browsable(false)]
		public string FallingBarsBrushSerializable
		{
			get { return Serialize.BrushToString(FallingBarsBrush); }
			set { FallingBarsBrush = Serialize.StringToBrush(value); }
		}	
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private TradingDJInsideBarParameter[] cacheTradingDJInsideBarParameter;
		public TradingDJInsideBarParameter TradingDJInsideBarParameter(int minimumConsecutiveBars, Brush risingBarsBrush, Brush fallingBarsBrush)
		{
			return TradingDJInsideBarParameter(Input, minimumConsecutiveBars, risingBarsBrush, fallingBarsBrush);
		}

		public TradingDJInsideBarParameter TradingDJInsideBarParameter(ISeries<double> input, int minimumConsecutiveBars, Brush risingBarsBrush, Brush fallingBarsBrush)
		{
			if (cacheTradingDJInsideBarParameter != null)
				for (int idx = 0; idx < cacheTradingDJInsideBarParameter.Length; idx++)
					if (cacheTradingDJInsideBarParameter[idx] != null && cacheTradingDJInsideBarParameter[idx].MinimumConsecutiveBars == minimumConsecutiveBars && cacheTradingDJInsideBarParameter[idx].RisingBarsBrush == risingBarsBrush && cacheTradingDJInsideBarParameter[idx].FallingBarsBrush == fallingBarsBrush && cacheTradingDJInsideBarParameter[idx].EqualsInput(input))
						return cacheTradingDJInsideBarParameter[idx];
			return CacheIndicator<TradingDJInsideBarParameter>(new TradingDJInsideBarParameter(){ MinimumConsecutiveBars = minimumConsecutiveBars, RisingBarsBrush = risingBarsBrush, FallingBarsBrush = fallingBarsBrush }, input, ref cacheTradingDJInsideBarParameter);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.TradingDJInsideBarParameter TradingDJInsideBarParameter(int minimumConsecutiveBars, Brush risingBarsBrush, Brush fallingBarsBrush)
		{
			return indicator.TradingDJInsideBarParameter(Input, minimumConsecutiveBars, risingBarsBrush, fallingBarsBrush);
		}

		public Indicators.TradingDJInsideBarParameter TradingDJInsideBarParameter(ISeries<double> input , int minimumConsecutiveBars, Brush risingBarsBrush, Brush fallingBarsBrush)
		{
			return indicator.TradingDJInsideBarParameter(input, minimumConsecutiveBars, risingBarsBrush, fallingBarsBrush);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.TradingDJInsideBarParameter TradingDJInsideBarParameter(int minimumConsecutiveBars, Brush risingBarsBrush, Brush fallingBarsBrush)
		{
			return indicator.TradingDJInsideBarParameter(Input, minimumConsecutiveBars, risingBarsBrush, fallingBarsBrush);
		}

		public Indicators.TradingDJInsideBarParameter TradingDJInsideBarParameter(ISeries<double> input , int minimumConsecutiveBars, Brush risingBarsBrush, Brush fallingBarsBrush)
		{
			return indicator.TradingDJInsideBarParameter(input, minimumConsecutiveBars, risingBarsBrush, fallingBarsBrush);
		}
	}
}

#endregion
