using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PlanetoidMP
{
	public class PlanetoidLogger
	{
		private LogLevel logLevel;
		private Type type;
		public PlanetoidLogger(Type type, LogLevel logLevel) {
			this.logLevel = logLevel;
			this.type = type;
		}

		public void Log(string msg)
		{
			if (this.logLevel == LogLevel.DEBUG)
			{
				Debug.Log("[" + type.ToString() + "] " + msg);
			}
		}
	}


	public enum LogLevel
	{
		DEBUG
	}
}