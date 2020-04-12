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
	public class TradingDJInsideBar : Indicator
	{
		#region Variables
		//Will keep the number of rising bars
		private int risingBarsCount;
		//Will keep the number of falling bars
		private int fallingBarsCount;
		//The number of minimum consecutive rising or falling bars preceding the inside bar
		private const int minimumConsecutiveBars = 3;
		#endregion
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"A simple Inside Bar Indicator preceded by three bars that make consecutive lower lows and lower highs or consecutive higher lows and higher highs.";
				Name										= "TradingDJInsideBar";
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
			if(risingBarsCount >= minimumConsecutiveBars
				&& High[0] <= High[1] && Low[0] >= Low[1])
			{
				//Here goes the logic for the pattern Inside Bar and minimumConsecutiveBars rising bars
				//We will simply set the color of the inside bar orange
				BarBrush = Brushes.Orange;
			}
			
			if(fallingBarsCount >= minimumConsecutiveBars
				&& High[0] <= High[1] && Low[0] >= Low[1])
			{
				//Here goes the logic for the pattern Inside Bar and minimumConsecutiveBars falling bars
				//We will simply set the color of the inside bar blue
				BarBrush = Brushes.Blue;
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
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private TradingDJInsideBar[] cacheTradingDJInsideBar;
		public TradingDJInsideBar TradingDJInsideBar()
		{
			return TradingDJInsideBar(Input);
		}

		public TradingDJInsideBar TradingDJInsideBar(ISeries<double> input)
		{
			if (cacheTradingDJInsideBar != null)
				for (int idx = 0; idx < cacheTradingDJInsideBar.Length; idx++)
					if (cacheTradingDJInsideBar[idx] != null &&  cacheTradingDJInsideBar[idx].EqualsInput(input))
						return cacheTradingDJInsideBar[idx];
			return CacheIndicator<TradingDJInsideBar>(new TradingDJInsideBar(), input, ref cacheTradingDJInsideBar);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.TradingDJInsideBar TradingDJInsideBar()
		{
			return indicator.TradingDJInsideBar(Input);
		}

		public Indicators.TradingDJInsideBar TradingDJInsideBar(ISeries<double> input )
		{
			return indicator.TradingDJInsideBar(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.TradingDJInsideBar TradingDJInsideBar()
		{
			return indicator.TradingDJInsideBar(Input);
		}

		public Indicators.TradingDJInsideBar TradingDJInsideBar(ISeries<double> input )
		{
			return indicator.TradingDJInsideBar(input);
		}
	}
}

#endregion
