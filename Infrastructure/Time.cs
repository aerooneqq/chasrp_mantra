using System;
using System.Diagnostics;

namespace Infrastructure
{
    public delegate void InvokingUnitDelegate();
    public static class Time
    {
        /// <summary>
        /// Measures the time which is needed to execute the unit of work.
        /// </summary>
        /// <param name="invokingUnit">The delegate which repesents the unit of work</param>
        /// <returns>The measured time as a TimeSpan object</returns>
        public static TimeSpan MeasureExecutionTime(InvokingUnitDelegate invokingUnit)
        {
            var stopwatch = Stopwatch.StartNew();

            invokingUnit?.Invoke();

            return stopwatch.Elapsed;
        }
    }
}
