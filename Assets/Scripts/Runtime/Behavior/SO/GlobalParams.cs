using UnityEngine;
using ZZZDemo.Runtime.Model.Config;

namespace ZZZDemo.Runtime.Behavior.SO
{
    [CreateAssetMenu(fileName = "GlobalParams", menuName = "ZZZDemo/Config/GlobalParam", order = 0)]
    public class GlobalParams : ScriptableObject
    {
	    public static GlobalParams instance;
	    public bool enable;
	    
	    public float smoothRotateAngleTolerance = 2.5f;
	    public float smoothRotateResponseTime = 0.04f;
        
	    public float walkToRunAccelerateFactor = 0.2f;
	    public float walkToRunSpeedThreshold = 0.6f;
        
	    public float canTurnBackTimeWindow = 0.15f;

	    public float continuousEvadeCooldownTime = 0.8f;
	    
        private void OnValidate()
		{
			ReloadParams();
		}

		public void EnableThis()
		{
			enable = true;
			if (instance != null && instance != this)
			{
				instance.enable = false;
			}
			instance = this;
		}

		public void ReloadParams()
		{
			if (!enable) return;

			EnableThis();

			GlobalConstants.smoothRotateAngleTolerance = smoothRotateAngleTolerance;
			GlobalConstants.smoothRotateResponseTime = smoothRotateResponseTime;
			GlobalConstants.walkToRunAccelerateFactor = walkToRunAccelerateFactor;
			GlobalConstants.walkToRunSpeedThreshold = walkToRunSpeedThreshold;
			GlobalConstants.canTurnBackTimeWindow = canTurnBackTimeWindow;
			GlobalConstants.continuousEvadeCooldownTime = continuousEvadeCooldownTime;
		}
    }
}