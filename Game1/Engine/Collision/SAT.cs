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
    class SAT
    {
        /// <summary>
        /// Checks if to list of vertices are overlapping
        /// </summary>
        /// <param name="shape1Verts"></param>
        /// <param name="shape2Verts"></param>
        /// <returns>True if there is any overlap</returns>
        public static bool Overlapping(List<Vector2> shape1Verts, List<Vector2> shape2Verts)
        {
            //We have to check the vertex between the ifrst and the last outside the loop
            Vector2 firstPoint = shape1Verts.First();
            Vector2 lastPoint = shape1Verts.Last();

            Normal norm = NormalOfVector(firstPoint, lastPoint);
            List<Vector2> shape1Projections = CalculateProjections(norm, shape1Verts);
            List<Vector2> shape2Projections = CalculateProjections(norm, shape2Verts);

            var orderedShape1Proj = shape1Projections.OrderBy(proj => proj.X + proj.Y).ToList();
            var orderedShape2Proj = shape2Projections.OrderBy(proj => proj.X + proj.Y).ToList();
            
            if ((orderedShape1Proj.Last().X + orderedShape1Proj.Last().Y) < (orderedShape2Proj.First().X + orderedShape2Proj.First().Y)
                || (orderedShape1Proj.First().X + orderedShape1Proj.First().Y) > (orderedShape2Proj.Last().X + orderedShape2Proj.Last().Y))
            {
                return false;
            }

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
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// Calculates the projections for a list of vertices against a normal
        /// </summary>
        /// <param name="norm"></param>
        /// <param name="verts"></param>
        /// <returns>List of projections</returns>
        private static List<Vector2> CalculateProjections(Normal norm, List<Vector2> verts)
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
        private static Vector2 ProjectionEquation(Vector2 vertexVector, Vector2 collisionAxisVector)
        {
            float vertexToCollisionAxisDot = DotProduct(collisionAxisVector, vertexVector);
            float collisionAxisDot = DotProduct(collisionAxisVector, collisionAxisVector);


            float dividedDots = vertexToCollisionAxisDot / collisionAxisDot;

            Vector2 projection = dividedDots * collisionAxisVector;

            return projection;
        }
    }
}
