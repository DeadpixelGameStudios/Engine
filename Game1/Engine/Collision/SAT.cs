using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Misc.MathsHelper;

namespace Engine.Collision
{
    /// <summary>
    /// Class with methods for seperating axis theorem
    /// </summary>
    public class SAT
    {
        /// <summary>
        /// Checks if to list of vertices are overlapping
        /// </summary>
        /// <param name="shape1Verts"></param>
        /// <param name="shape2Verts"></param>
        /// <returns>Vector2 of smallest overlap</returns>
        public Vector2 Overlapping(List<Vector2> shape1Verts, List<Vector2> shape2Verts)
        {
            //We have to check the vertex between the first and the last outside the loop
            Vector2 firstPoint = shape1Verts.First();
            Vector2 lastPoint = shape1Verts.Last();

            Normal norm = NormalOfVector(firstPoint, lastPoint); //should this be last to first? does it matter? - probably not
            List<Vector2> shape1Projections = CalculateProjections(norm, shape1Verts);
            List<Vector2> shape2Projections = CalculateProjections(norm, shape2Verts);

            var orderedShape1Proj = shape1Projections.OrderBy(proj => proj.X + proj.Y).ToList();
            var orderedShape2Proj = shape2Projections.OrderBy(proj => proj.X + proj.Y).ToList();

            #region first and last
            if ((orderedShape1Proj.Last().X + orderedShape1Proj.Last().Y) < (orderedShape2Proj.First().X + orderedShape2Proj.First().Y)
                || (orderedShape1Proj.First().X + orderedShape1Proj.First().Y) > (orderedShape2Proj.Last().X + orderedShape2Proj.Last().Y))
            {
                return default(Vector2);
            }

            var mtvList = new List<Vector2>();
            if ((orderedShape1Proj.Last().X + orderedShape1Proj.Last().Y) >= (orderedShape2Proj.Last().X + orderedShape2Proj.Last().Y))
            {
                mtvList.Add(CalcualateMTV(norm, orderedShape1Proj.First(), orderedShape2Proj.Last()));
            }
            else
            {
                mtvList.Add(CalcualateMTV(norm, orderedShape2Proj.First(), orderedShape1Proj.Last()));
            }
            #endregion

            for (int i = 0; i <= shape1Verts.Count - 2; i++)
            {
                norm = NormalOfVector(shape1Verts[i], shape1Verts[i + 1]);

                shape1Projections = CalculateProjections(norm, shape1Verts);
                shape2Projections = CalculateProjections(norm, shape2Verts);

                orderedShape1Proj = shape1Projections.OrderBy(proj => proj.X + proj.Y).ToList();
                orderedShape2Proj = shape2Projections.OrderBy(proj => proj.X + proj.Y).ToList();

                if ((orderedShape1Proj.Last().X + orderedShape1Proj.Last().Y) < (orderedShape2Proj.First().X + orderedShape2Proj.First().Y)
                    || (orderedShape1Proj.First().X + orderedShape1Proj.First().Y) > (orderedShape2Proj.Last().X + orderedShape2Proj.Last().Y))
                {
                    return default(Vector2);
                }


                //var mtv = CalcualateMTV(norm, orderedShape1Proj.First(), orderedShape2Proj.Last());
                ////Console.WriteLine("shape 1 first - " + mtv);
                //mtvList.Add(mtv);

                if ((orderedShape1Proj.Last().X + orderedShape1Proj.Last().Y) >= (orderedShape2Proj.Last().X + orderedShape2Proj.Last().Y))
                {
                    var mtv = CalcualateMTV(norm, orderedShape1Proj.First(), orderedShape2Proj.Last());
                    //Console.WriteLine("shape 1 first - " + mtv);
                    mtvList.Add(mtv);
                }
                else
                {
                    var mtv = CalcualateMTV(norm, orderedShape2Proj.First(), orderedShape1Proj.Last());
                    //Console.WriteLine("shape 2 first - " + mtv);
                    mtvList.Add(mtv);
                }
            }

            var nearest = mtvList.OrderBy(x => Math.Abs((x.X + x.Y) - 0)).First();

            return nearest;

            //commenting out while i try the above method to find the value closest to 0

            //var orderedMTVList = mtvList.OrderBy(mtv => mtv.X + mtv.Y).ToList();

            ////var nearest = mtvList.OrderBy(x => Math.Abs((x.X + x.Y) - 0)).First();

            //var firstMTV = orderedMTVList.First();

            //if(firstMTV.X == 50 || firstMTV.X == -50 || firstMTV.Y == 50 || firstMTV.Y == -50)
            //{
            //    orderedMTVList.Remove(firstMTV);
            //}


            //return orderedMTVList.First();
        }


        private Vector2 CalcualateMTV(Normal norm, Vector2 shape1First, Vector2 shape2Last)
        {
            Vector2 gradient = norm.point2 - norm.point1;
            gradient.Normalize();

            //float overlap = (shape1First.X + shape1First.Y) - (shape2Last.X + shape2Last.Y);
            float overlap = (shape2Last.X + shape2Last.Y) - (shape1First.X + shape1First.Y);

            return gradient * overlap;
        }

        /// <summary>
        /// Calculates the projections for a list of vertices against a normal
        /// </summary>
        /// <param name="norm"></param>
        /// <param name="verts"></param>
        /// <returns>List of projections</returns>
        private List<Vector2> CalculateProjections(Normal norm, List<Vector2> verts)
        {
            List<Vector2> projectionList = new List<Vector2>();
            foreach (Vector2 vert in verts)
            {
                projectionList.Add(ProjectionEquation(vert, norm.point1));
            }

            return projectionList;
        }

        /// <summary>
        /// Projection equation for SAT
        /// ( CollisionAxis . VertexVector / CollisionAxis . CollisionAxis ) CollisionAxis
        /// </summary>
        /// <param name="vertexVector"></param>
        /// <param name="collisionAxisVector"></param>
        /// <returns>Returns the projection</returns>
        private Vector2 ProjectionEquation(Vector2 vertexVector, Vector2 collisionAxisVector)
        {
            float vertexToCollisionAxisDot = DotProduct(collisionAxisVector, vertexVector);
            float collisionAxisDot = DotProduct(collisionAxisVector, collisionAxisVector);


            float dividedDots = vertexToCollisionAxisDot / collisionAxisDot;

            Vector2 projection = dividedDots * collisionAxisVector;

            return projection;
        }
    }
}
