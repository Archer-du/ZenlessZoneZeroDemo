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
        internal class CharacterTimer
        {
            internal bool isDead;
            private readonly float threshold;
            internal float value;
            internal Action action;
            internal bool loop;

            internal CharacterTimer(float value, Action action, bool loop)
            {
                isDead = false;
                threshold = value;
                this.value = threshold;
                this.action = action;
                this.loop = loop;
            }

            internal void Update(float deltaTime)
            {
                if (value > 0f)
                {
                    value -= deltaTime;
                }
                else
                {
                    action?.Invoke();
                    if (loop)
                    {
                        Reset();
                    }
                    else
                    {
                        isDead = true;
                    }
                }
            }

            private void Reset() => value = threshold;
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
                timer.Update(deltaTime);
                if(timer.isDead)
                    keysToRemove.Add(kvp.Key);
            }

            foreach (var guid in keysToRemove)
            {
                timerMap.Remove(guid);
            }
        }

        internal TimerHandle SetTimer(float time, Action action, bool loop = false)
        {
            var guid = Guid.NewGuid();
            TimerHandle timerHandle = new TimerHandle(this, guid);
            if (!timerMap.TryAdd(guid, new CharacterTimer(time, action, loop)))
            {
                return null;
            }
            return timerHandle;
        }
    }
}