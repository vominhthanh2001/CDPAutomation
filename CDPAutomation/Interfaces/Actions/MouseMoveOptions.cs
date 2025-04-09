using CDPAutomation.Enums.FindElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Actions
{
    internal class MouseMoveOptions
    {
        private static readonly Random _random = new();

        /// <summary>
        /// Tọa độ X điểm bắt đầu di chuyển chuột.
        /// </summary>
        public double StartX { get; set; }

        /// <summary>
        /// Tọa độ Y điểm bắt đầu di chuyển chuột.
        /// </summary>
        public double StartY { get; set; }

        /// <summary>
        /// Tọa độ X điểm kết thúc di chuyển chuột.
        /// </summary>
        public double EndX { get; set; }

        /// <summary>
        /// Tọa độ Y điểm kết thúc di chuyển chuột.
        /// </summary>
        public double EndY { get; set; }

        /// <summary>
        /// Kiểu thuật toán di chuyển chuột (Bezier, ZigZag, Gaussian, v.v.).
        /// </summary>
        public MouseMoveAction Action { get; set; } = MouseMoveAction.Linear;

        /// <summary>
        /// Số bước (step) chia nhỏ hành trình chuột. Nếu null sẽ tự động tính theo khoảng cách và độ mịn.
        /// </summary>
        public int? Steps { get; set; } = null;

        /// <summary>
        /// Độ mịn (smoothness) của chuyển động: càng lớn thì càng nhiều bước => di chuyển mượt hơn.
        /// </summary>
        public double Smoothness { get; set; } = 1.0;

        /// <summary>
        /// Bật mô phỏng profile tốc độ giống người (khởi đầu chậm, giữa nhanh, kết thúc chậm).
        /// Sẽ được random khi khởi tạo (30~70% cơ hội bật).
        /// </summary>
        public bool HumanLikeSpeedProfile { get; set; } = _random.NextDouble() < 0.5;

        /// <summary>
        /// Bật hiệu ứng "rung" nhỏ khi di chuyển chuột để giống tay người run nhẹ.
        /// Sẽ được random khi khởi tạo (10~30% cơ hội bật).
        /// </summary>
        public bool UseRealisticWobble { get; set; } = _random.NextDouble() < 0.2;

        /// <summary>
        /// Cường độ nhiễu ngẫu nhiên trên trục X (mỗi điểm sẽ bị lệch nhẹ theo X).
        /// </summary>
        public double RandomNoiseIntensityX { get; set; } = 0.0;

        /// <summary>
        /// Cường độ nhiễu ngẫu nhiên trên trục Y (mỗi điểm sẽ bị lệch nhẹ theo Y).
        /// </summary>
        public double RandomNoiseIntensityY { get; set; } = 0.0;
    }

}
