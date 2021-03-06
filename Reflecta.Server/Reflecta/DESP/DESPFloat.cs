﻿#region Using

using System;

#endregion

namespace Reflecta
{
    public sealed class DESPFloat
    {
        public float alpha = 0.33f;
        private float crt_stat_1;
        private float crt_stat_2;
        private float prev_stat_1;
        private float prev_stat_2;

        public void Update(float value)
        {
            crt_stat_1 = alpha*value + (1 - alpha)*prev_stat_1;
            crt_stat_2 = alpha*crt_stat_1 + (1 - alpha)*prev_stat_2;

            prev_stat_1 = crt_stat_1;
            prev_stat_2 = crt_stat_2;
        }

        public float Predict(int tau)
        {
            var factor = alpha/(1 - alpha)*tau;
            return (2 + factor)*crt_stat_1 - (1 + factor)*crt_stat_2;
        }

        public float Predict(float tau)
        {
            var floor = (int) Math.Floor(tau);
            var ceiling = (int) Math.Ceiling(tau);

            var lo = Predict(floor);
            var hi = Predict(ceiling);

            var rho = tau - floor;

            return MathHelper.Lerp(lo, hi, rho);
        }
    }
}