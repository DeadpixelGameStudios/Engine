using System.Collections.Generic;
using System.Linq;
using Game1.Engine.Entity;
using Game1.Engine.Managers;
using Game1.Engine.Misc;
using Game1.Engine.Shape;
using Microsoft.Xna.Framework;
using static Game1.Engine.Misc.MathsHelper;
using static Game1.Engine.Collision.SAT;

namespace Game1
{
    public class CollisionManager: iCollisionManager, iManager
    {
        private List<IShape> collidableList = new List<IShape>();
        private List<IShape> collisionListeners = new List<IShape>();


        public void AddCollidable(IShape collidable)
        {
            //will have some logic to add things to quadtree
            //do we still need to sort out scenegraph?

            if (collidable.IsCollisionListener())
            {
                collisionListeners.Add(collidable);
                return;
            }

            collidableList.Add(collidable);
        }

        private List<IShape> BroadPhase(IShape shape)
        {
            return collidableList;
        }


        private void MidPhase(List<IShape> midList, IShape shape)
        {
            foreach (IShape col in midList)
            {
                if (col.GetBoundingBox().Intersects(shape.GetBoundingBox()))
                {
                    //Checks to see whether the vertices of the shape are the same as the bounding box
                    if (!isSameAsBoundingBox(col) || !isSameAsBoundingBox(shape))
                    {
                        //Go to narrow phase (SAT) if either shape isnt
                        NarrowPhase(col, shape);
                    }
                    else
                    {
                        //collision alert - events or not? not sure yet, but probably
                    }
                }

            }
        }


        private void NarrowPhase(IShape col1, IShape col2)
        {
            //Get list of each shapes vertices
            List<Vector2> shape1Vertices = col1.GetVertices();
            List<Vector2> shape2Vertices = col2.GetVertices();

            //Add the shape position to each vertex
            List<Vector2> shape1VertPosition = new List<Vector2>();
            foreach(var vert in shape1Vertices)
            {
                shape1VertPosition.Add(col1.GetPosition() + vert);
            }

            List<Vector2> shape2VertPosition = new List<Vector2>();
            foreach (var vert in shape2Vertices)
            {
                shape2VertPosition.Add(col2.GetPosition() + vert);
            }

            //Check for overlap
            bool shape1Overlap = Overlapping(shape1VertPosition, shape2VertPosition);
            bool shape2Overlap = Overlapping(shape2VertPosition, shape1VertPosition);

            //Test code to add transparency if theres overlap
            if(!shape1Overlap || !shape2Overlap)
            {
                //not colliding
                iEntity ghj = (iEntity)col2;
                ghj.Transparency = 1f;
            }
            else
            {
                //colliding
                iEntity ghj = (iEntity)col2;
                ghj.Transparency = 0.5f;
            }
        }

        private void CollisionPhases()
        {
            foreach(IShape collisionListener in collisionListeners)
            {
                //List of shapes returned from the broad phase
                List<IShape> broadPhase = BroadPhase(collisionListener);

                //Mid phase basic AABB
                MidPhase(broadPhase, collisionListener);
            }
        }

        public void Update()
        {
            CollisionPhases();
        }

    }
}