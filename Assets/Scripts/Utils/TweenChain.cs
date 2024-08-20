using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class TweenChain
    {
        public Queue<Sequence> SequenceQueue = new Queue<Sequence>();

        public TweenChain()
        {
            
        }

        public void AddAndPlay(Tween tween)
        {
            var sequence = DOTween.Sequence();
            sequence.Pause();
            sequence.Append(tween);
            SequenceQueue.Enqueue(sequence);
            if (SequenceQueue.Count == 1)
            {
                SequenceQueue.Peek().Play();
            }

            sequence.OnComplete(OnSequenceComplete);
        }

        private void OnSequenceComplete()
        {
            SequenceQueue.Dequeue();
            if (SequenceQueue.Count > 0)
            {
                SequenceQueue.Peek().Play();
            }
        }

        public bool IsRunning()
        {
            return SequenceQueue.Count > 0;
        }

        public void Destroy()
        {
            foreach (var sequence in SequenceQueue)
            {
                sequence.Kill();
            }
            SequenceQueue.Clear();
        }
    }
}
