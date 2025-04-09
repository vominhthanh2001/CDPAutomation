using CDPAutomation.Enums.FindElement;
using CDPAutomation.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Extensions
{
    internal static class MouseMovementGenerator
    {
        public static List<MousePosition> GeneratePath(MouseMoveOptions options)
        {
            int steps = options.Steps ?? AutoSteps(options);

            return options.Action switch
            {
                MouseMoveAction.Bezier => BezierPath(options, steps),
                MouseMoveAction.ZigZag => ZigZagPath(options, steps),
                MouseMoveAction.Gaussian => GaussianPath(options, steps),
                MouseMoveAction.Random => RandomPath(options, steps),
                MouseMoveAction.Default => LinearPath(options, steps),
                MouseMoveAction.HumanLike => BezierPath(options, steps),// Temporary fallback to Bezier
                MouseMoveAction.Spiral => SpiralPath(options, steps),
                MouseMoveAction.Linear => LinearPath(options, steps),
                MouseMoveAction.ImpulseResponse => ImpulsePath(options, steps),
                MouseMoveAction.BodyMimicry => BodyMimicryPath(options, steps),
                _ => LinearPath(options, steps),
            };
        }

        private static int AutoSteps(MouseMoveOptions o)
        {
            double dx = o.EndX - o.StartX;
            double dy = o.EndY - o.StartY;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return Math.Max(10, (int)(distance / (5.0 / o.Smoothness)));
        }

        private static List<MousePosition> ApplyDelays(List<MousePosition> path, MouseMoveOptions options)
        {
            var r = new Random();
            var delayedPath = new List<MousePosition>();

            // GetDelay cơ bản tính theo smoothness hoặc số bước
            int avgDelay = (int)(15 / Math.Max(0.5, options.Smoothness)); // càng mượt thì delay càng ít

            foreach (var pos in path)
            {
                delayedPath.Add(new MousePosition
                {
                    X = pos.X,
                    Y = pos.Y,
                    Delay = r.Next(avgDelay - 3, avgDelay + 5) // dao động nhẹ tạo tự nhiên
                });
            }

            return delayedPath;
        }

        private static List<MousePosition> LinearPath(MouseMoveOptions o, int steps)
        {
            var path = new List<MousePosition>();
            for (int i = 0; i <= steps; i++)
            {
                double t = Ease(o, i, steps);
                double x = Lerp(o.StartX, o.EndX, t) + Noise(o);
                double y = Lerp(o.StartY, o.EndY, t) + Noise(o, true);
                path.Add(new MousePosition { X = (int)x, Y = (int)y });
            }

            return ApplyDelays(path, o);
        }

        private static List<MousePosition> BezierPath(MouseMoveOptions o, int steps)
        {
            var path = new List<MousePosition>();
            double cx = (o.StartX + o.EndX) / 2 + 50;
            double cy = (o.StartY + o.EndY) / 2 - 50;

            for (int i = 0; i <= steps; i++)
            {
                double t = Ease(o, i, steps);
                double x = Math.Pow(1 - t, 2) * o.StartX + 2 * (1 - t) * t * cx + Math.Pow(t, 2) * o.EndX + Noise(o);
                double y = Math.Pow(1 - t, 2) * o.StartY + 2 * (1 - t) * t * cy + Math.Pow(t, 2) * o.EndY + Noise(o, true);
                path.Add(new MousePosition { X = (int)x, Y = (int)y });
            }
            return ApplyDelays(path, o);
        }

        private static List<MousePosition> ZigZagPath(MouseMoveOptions o, int steps)
        {
            var path = new List<MousePosition>();
            double amplitude = 10.0;
            for (int i = 0; i <= steps; i++)
            {
                double t = Ease(o, i, steps);
                double x = Lerp(o.StartX, o.EndX, t);
                double y = Lerp(o.StartY, o.EndY, t) + Math.Sin(t * 10 * Math.PI) * amplitude + Noise(o, true);
                path.Add(new MousePosition { X = (int)(x + Noise(o)), Y = (int)y });
            }

            return ApplyDelays(path, o);
        }

        private static List<MousePosition> GaussianPath(MouseMoveOptions o, int steps)
        {
            var path = new List<MousePosition>();
            Random rand = new();
            for (int i = 0; i <= steps; i++)
            {
                double t = Ease(o, i, steps);
                double x = Lerp(o.StartX, o.EndX, t) + rand.NextDouble() * 2 - 1 + Noise(o);
                double y = Lerp(o.StartY, o.EndY, t) + rand.NextDouble() * 2 - 1 + Noise(o, true);
                path.Add(new MousePosition { X = (int)x, Y = (int)y });
            }

            return ApplyDelays(path, o);
        }

        private static List<MousePosition> RandomPath(MouseMoveOptions o, int steps)
        {
            var rand = new Random();
            var path = new List<MousePosition>();
            double currentX = o.StartX, currentY = o.StartY;
            for (int i = 0; i < steps; i++)
            {
                double t = Ease(o, i, steps);
                double tx = Lerp(o.StartX, o.EndX, t);
                double ty = Lerp(o.StartY, o.EndY, t);
                currentX += (tx - currentX) * 0.5 + rand.NextDouble() * 5 - 2.5 + Noise(o);
                currentY += (ty - currentY) * 0.5 + rand.NextDouble() * 5 - 2.5 + Noise(o, true);
                path.Add(new MousePosition { X = (int)currentX, Y = (int)currentY });
            }
            path.Add(new MousePosition { X = (int)o.EndX, Y = (int)o.EndY });

            return ApplyDelays(path, o);
        }

        private static List<MousePosition> SpiralPath(MouseMoveOptions o, int steps)
        {
            var path = new List<MousePosition>();
            double centerX = (o.StartX + o.EndX) / 2;
            double centerY = (o.StartY + o.EndY) / 2;
            double radius = Math.Sqrt(Math.Pow(o.EndX - o.StartX, 2) + Math.Pow(o.EndY - o.StartY, 2)) / 2;

            for (int i = 0; i <= steps; i++)
            {
                double t = (double)i / steps;
                double angle = t * 4 * Math.PI; // 2 turns
                double r = radius * t;
                double x = centerX + r * Math.Cos(angle) + Noise(o);
                double y = centerY + r * Math.Sin(angle) + Noise(o, true);
                path.Add(new MousePosition { X = (int)x, Y = (int)y });
            }

            return ApplyDelays(path, o);
        }

        private static List<MousePosition> ImpulsePath(MouseMoveOptions o, int steps)
        {
            return LinearPath(o, steps); // Placeholder
        }

        private static List<MousePosition> BodyMimicryPath(MouseMoveOptions o, int steps)
        {
            return BezierPath(o, steps); // Placeholder
        }

        private static double Ease(MouseMoveOptions o, int i, int steps)
        {
            double t = (double)i / steps;
            if (!o.HumanLikeSpeedProfile) return t;
            return t * t * (3 - 2 * t); // smoothstep easing
        }

        private static double Lerp(double a, double b, double t) => a + (b - a) * t;

        private static double Noise(MouseMoveOptions o, bool vertical = false)
        {
            var r = new Random();
            double intensity = vertical ? o.RandomNoiseIntensityY : o.RandomNoiseIntensityX;
            return (r.NextDouble() * 2 - 1) * intensity;
        }
    }
}
