using System.Collections.Generic;
using Engine.Entity;
using Engine.Managers;
using Engine.Shape;
using Microsoft.Xna.Framework;
using static Engine.Misc.MathsHelper;
using static Engine.Collision.SAT;

namespace Engine.Collision
{
    public class CollisionManager: iCollisionManager, iManager
    {
        private List<IShape> collidableList = new List<IShape>();
        private List<IShape> collisionListeners = new List<IShape>();
        private List<IShape> PotentialCollisions = new List<IShape>();
        private QuadTree qTree = new QuadTree(1, new Rectangle(0, 0, EngineMain.ScreenWidth, EngineMain.ScreenHeight));

       

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

        private void AddToQuadTree()
        {
            if (collidableList != null)
            {
                foreach (IShape collidable in collidableList)
                {
                    qTree.Insert(collidable);
                }
            }
           
        }

        private void QuadTreeUpdate()
        {
            
            qTree.Clear();
            AddToQuadTree();
            
        }

        private List<IShape> BroadPhase(IShape shape)
        {
            QuadTreeUpdate();

            qTree.FindPossibleCollisions(PotentialCollisions, shape);

            foreach (IShape ent in PotentialCollisions)
            {
                //potential colliders

                //iEntity potEnt = (iEntity)ent;
                //potEnt.Transparency = 0.5f;
                
            }
            return PotentialCollisions;
        }

        private void MidPhase(List<IShape> midList, IShape shape)
        {
            foreach (IShape col in midList)
            {
                iEntity Collider = (iEntity)shape;
                
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
                        Collider.CollidingEntity = (iEntity)col;
                        Collider.isColliding = true;
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
            PotentialCollisions.Clear();

            foreach (IShape collisionListener in collisionListeners)
            {
                //List of shapes returned from the broad phase
                List<IShape> broadPhase = BroadPhase(collisionListener);

                //Mid phase basic AABB
                MidPhase(broadPhase, collisionListener);
            }
        }

        public void Update()
        {
            foreach (IShape ent in collidableList)
            {
                if (!PotentialCollisions.Contains((iEntity)ent))
                {
                    iEntity tmp = (iEntity)ent;
                    tmp.Transparency = 1f;
                }
            }

            CollisionPhases();
        }

    }
}