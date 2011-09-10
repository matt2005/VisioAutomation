﻿using System.Collections.Generic;
using System.Linq;
using VA=VisioAutomation;

namespace VisioAutomation.Drawing
{
    public struct BezierSegment
    {
        public VA.Drawing.Point Start { get; private set; }
        public VA.Drawing.Point Handle1 { get; private set; }
        public VA.Drawing.Point Handle2 { get; private set; }
        public VA.Drawing.Point End { get; private set; }

        public BezierSegment(VA.Drawing.Point start, VA.Drawing.Point handle1, VA.Drawing.Point handle2, VA.Drawing.Point end)
            : this()
        {
            this.Start = start;
            this.Handle1 = handle1;
            this.Handle2 = handle2;
            this.End = end;
        }

        public BezierSegment(IList<VA.Drawing.Point> points)
            : this()
        {
            if (points == null)
            {
                throw new System.ArgumentNullException("points");
            }

            if (points.Count != 4)
            {
                throw new System.ArgumentException("Must have exactly 4 points");
            }

            this.Start = points[0];
            this.Handle1 = points[1];
            this.Handle2 = points[2];
            this.End = points[3];
        }

        public static IList<VA.Drawing.Point> Merge(IList<BezierSegment> segments, out int degree)
        {
            if (segments == null)
            {
                throw new System.ArgumentNullException("segments");
            }

            var points = new List<VA.Drawing.Point>(segments.Count * 4);
            int n = 0;
            foreach (var seg in segments)
            {
                if (n == 0)
                {
                    points.Add(seg.Start);
                }

                points.Add(seg.Handle1);
                points.Add(seg.Handle2);
                points.Add(seg.End);
                n++;
            }

            degree = 3;
            return points;
        }

        public static BezierSegment[] FromArc(VA.Angle startangle, VA.Angle endangle)
        {
            if (endangle.Radians < startangle.Radians)
            {
                throw new System.ArgumentOutOfRangeException("endangle must be >= startangle");
            }

            VA.Angle min_angle = VA.Angle.FromRadians(0);
            VA.Angle max_angle = VA.Angle.FromRadians(System.Math.PI*2);

            VA.Angle  total_angle = endangle - startangle;

            if (total_angle.Radians == min_angle.Radians)
            {
                var arr = new BezierSegment[1];
                double cos_theta = System.Math.Cos(startangle.Radians);
                double sin_theta = System.Math.Sin(startangle.Radians);
                var p0 = new VA.Drawing.Point(cos_theta, -sin_theta);
                var p1 = RotateAroundOrigin( p0, startangle.Radians);
                arr[0] = new BezierSegment(p1,p1,p1,p1);
            }

            if (total_angle.Radians > max_angle.Radians)
            {
                endangle = startangle + max_angle;
            }

            var bez_arr = subdivide_arc_nicely(startangle.Radians, endangle.Radians)
                .Select(a => get_bezier_points_for_small_arc(a.begin, a.end))
                .ToArray();

            return bez_arr;
        }

        private static IEnumerable<VA.Internal.ArcSegment> subdivide_arc_nicely(double start_angle, double end_angle)
        {
            // TODO: Should calculate number of subarcs without resorting to an enumeration
            // TODO: add test cases

            if (start_angle > end_angle)
            {
                throw new System.ArgumentException("end_angle must be less than start angle", "end_angle");
            }

            // the original purpose of this method is to break apart arcs > 90 degrees into smaller sub-arcs of 90 or less
            // the current implementation does that but also fractures the arc on 0,90,180,270, etc. degrees even if the arc length is <90
            // example (85,110) -> (85,90) & (90,110)

            double right_angle = System.Math.PI/2;

            var cur = new VA.Internal.ArcSegment(start_angle, end_angle);
            while (true)
            {
                double temp = System.Math.Floor(cur.begin/right_angle);
                double cut_angle = (temp + 1)*right_angle;

                if ((cur.begin < cut_angle) && (cut_angle < cur.end))
                {
                    yield return (new VA.Internal.ArcSegment(cur.begin, cut_angle));
                    cur = new VA.Internal.ArcSegment(cut_angle, cur.end);
                }
                else
                {
                    yield return cur;
                    break;
                }
            }
        }

        private static BezierSegment get_bezier_points_for_small_arc(double start_angle, double end_angle)
        {
            const double right_angle = System.Math.PI/2;
            double total_angle = end_angle - start_angle;

            if (total_angle > right_angle)
            {
                throw new System.ArgumentOutOfRangeException("end_angle",
                                                             "angle formed by start and end must <= right angle (pi/2)");
            }

            double theta = (end_angle - start_angle)/2;
            double cos_theta = System.Math.Cos(theta);
            double sin_theta = System.Math.Sin(theta);

            var p0 = new VA.Drawing.Point(cos_theta, -sin_theta);
            var p1 = new VA.Drawing.Point((4 - cos_theta) / 3.0, ((1 - cos_theta) * (cos_theta - 3.0)) / (3 * sin_theta));
            var p2 = new VA.Drawing.Point(p1.X, -p1.Y);
            var p3 = new VA.Drawing.Point(p0.X, -p0.Y);

            var arc_bezier = new[] {p0, p1, p2, p3}
                .Select(p => RotateAroundOrigin(p, theta + start_angle))
                .ToArray();

            return new BezierSegment(arc_bezier);
        }

        private static Drawing.Point RotateAroundOrigin(Drawing.Point p1, double theta)
        {
            double nx = (System.Math.Cos(theta)*p1.X) - (System.Math.Sin(theta)*p1.Y);
            double ny = (System.Math.Sin(theta)*p1.X) + (System.Math.Cos(theta)*p1.Y);
            return new Drawing.Point(nx, ny);
        }
    }
}