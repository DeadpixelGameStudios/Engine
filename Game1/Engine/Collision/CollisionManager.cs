using System.Collections.Generic;
using Engine.Entity;
using Engine.Managers;
using Engine.Shape;
using Microsoft.Xna.Framework;
using static Engine.Misc.MathsHelper;
using static Engine.Collision.SAT;
using System;
using Engine.Engine.Collision;

namespace Engine.Collision
{
    public class CollisionManager: iCollisionManager, iManager
    {
        #region Members
        private List<IShape> collidableList = new List<IShape>();
        private List<IShape> collisionListeners = new List<IShape>();
        private List<IShape> PotentialCollisions = new List<IShape>();
        private QuadTree qTree = new QuadTree(1, new Rectangle(0, 0, EngineMain.ScreenWidth, EngineMain.ScreenHeight));
        public event EventHandler<CollisionDetails> RaiseCollision;
        private SAT sat = new SAT();
        #endregion

        public void AddCollidable(IShape collidable)
        {
            if(!(collidable is ICollisionListener))
            {
                collidableList.Add(collidable);
            }
        }

        public void AddCollisionListener(IShape colListener)
        {
            collisionListeners.Add(colListener);
        }

        public void Remove(IShape collidable)
        {
            collisionListeners.Remove(collidable);
            collidableList.Remove(collidable);
        }


        #region QuadTree
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
        #endregion


        private List<IShape> BroadPhase(IShape shape)
        {
            QuadTreeUpdate();

            qTree.FindPossibleCollisions(PotentialCollisions, shape);
            
            return PotentialCollisions;
        }

        private void MidPhase(List<IShape> midList, IShape shape)
        {
            foreach (IShape col in midList)
            {
                iEntity Collider = (iEntity)shape;
                
                if (col.GetBoundingBox().Intersects(shape.GetBoundingBox()))
                {

                    NarrowPhase(col, shape);

                    //Checks to see whether the vertices of the shape are the same as the bounding box
                    if (!isSameAsBoundingBox(col) || !isSameAsBoundingBox(shape))
                    {
                        //Go to narrow phase (SAT) if either shape isnt
                        //NarrowPhase(col, shape);
                    }
                    else
                    {
                        //collision alert - events or not? not sure yet, but probably
                        //Collider.CollidingEntity = (iEntity)col;
                        //Collider.isColliding = true;
                        
                        var ent = (iEntity)shape;
                        var ent2 = (iEntity)col;

                        //OnRaiseCollision(new CollisionDetails(ent.UName, ent2));

                        //Console.WriteLine("colliding things? " + ent.UName + " - " + ent2.UName);
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
            var shape1MTV = sat.Overlapping(shape1VertPosition, shape2VertPosition);
            var shape2MTV = sat.Overlapping(shape2VertPosition, shape1VertPosition);
            
            //Test code to add transparency if theres overlap
            if (shape1MTV == default(Vector2) || shape2MTV == default(Vector2))
            {
                //not colliding
            }
            else
            {
                var ent = (iEntity)col2;
                var ent2 = (iEntity)col1;

                var smallestMTV = GreaterVector2(shape1MTV, shape2MTV) == shape1MTV ? shape2MTV : shape1MTV;

                var col1Center = col1.GetBoundingBox().Center;
                var col2Center = col2.GetBoundingBox().Center;

                if(smallestMTV.X == 0)
                {
                    if (col1Center.Y < col2Center.Y)
                    {
                        smallestMTV *= -1;
                    }
                }
                
                if(smallestMTV.Y == 0)
                {
                    if (col1Center.X > col2Center.X)
                    {
                        smallestMTV *= -1;
                    }
                }

                OnRaiseCollision(new CollisionDetails(ent.UName, ent2, smallestMTV));
            }
        }


        public void OnRaiseCollision(CollisionDetails colDetails)
        {
            RaiseCollision?.Invoke(this, colDetails);
        }

        private void CollisionPhases()
        {
            PotentialCollisions.Clear();

            foreach (IShape collisionListener in collisionListeners)
            {
                //List of shapes returned from the broad phase
                List<IShape> broadPhase = BroadPhase(collisionListener);

                //Mid phase basic AABB - raises NarrowPhase if needed
                MidPhase(broadPhase, collisionListener);
            }
        }

        public void Update()
        {
            CollisionPhases();
        }

    }
}