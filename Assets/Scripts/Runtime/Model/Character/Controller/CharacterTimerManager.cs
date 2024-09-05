using System;
using System.Collections.Generic;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    internal class TimerHandle
    {
        private Guid id;
        private CharacterTimerManager ownerManager;
        internal TimerHandle(CharacterTimerManager ownerManager, Guid id)
        {
            this.id = id;
            this.ownerManager = ownerManager;
        }

        internal bool IsValid() => ownerManager.timerMap.ContainsKey(id);

        internal bool Invalidate()
        {
            if (!IsValid()) return false;
            ownerManager.timerMap.Remove(id);
            return true;
        }
    }
    internal class CharacterTimerManager
    {
        internal struct CharacterTimer
        {
            internal float value;
            internal Action action;

            internal CharacterTimer(float value, Action action)
            {
                this.value = value;
                this.action = action;
            }
        }

        private CharacterController controller;

        internal Dictionary<Guid, CharacterTimer> timerMap;
        
        internal CharacterTimerManager(CharacterController controller)
        {
            this.controller = controller;
            
            timerMap = new();
        }

        internal void Update(float deltaTime)
        {
            var keysToRemove = new List<Guid>();
            foreach (var kvp in timerMap)
            {
                var timer = kvp.Value;
                if (timer.value > 0f)
                {
                    timer.value -= deltaTime;
                }
                else
                {
                    timer.action?.Invoke();
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var guid in keysToRemove)
            {
                timerMap.Remove(guid);
            }
        }

        internal TimerHandle SetTimer(float time, Action action)
        {
            var guid = Guid.NewGuid();
            TimerHandle timerHandle = new TimerHandle(this, guid);
            if (!timerMap.TryAdd(guid, new CharacterTimer(time, action)))
            {
                // TODO: LogError
                return null;
            }
            return timerHandle;
        }
    }
}