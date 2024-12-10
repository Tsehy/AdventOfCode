namespace AdventOfCode
{
    public class Node
    {
        public bool IsLeaf { get; set; }
        public int? Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public int Depth
        {
            get
            {
                Node tmp = this;
                int depth = 0;

                while (tmp.Parent != null)
                {
                    tmp = tmp.Parent;
                    depth++;
                }
                return depth;
            }
        }

        public Node(int value, Node parent)
        {
            Value = value;
            IsLeaf = true;
            Parent = parent;
            Left = null;
            Right = null;
        }

        public Node(Node left, Node right, Node parent)
        {
            Value = null;
            IsLeaf = false;
            Parent = parent;
            Left = left;
            Left.Parent = this;
            Right = right;
            Right.Parent = this;
        }

        public void Simplify()
        {
            while (true)
            {
                Node explodeable = FindExplodable();
                if (explodeable != null)
                {
                    explodeable.Explode();
                    continue;
                }

                Node splitable = FindSplitable();
                if (splitable != null)
                {
                    splitable.Split();
                    continue;
                }

                break;
            }
        }

        public void Explode()
        {
            Node leftNeighbour = FindLeftNeighbour();
            if (leftNeighbour != null && leftNeighbour.IsLeaf)
            {
                leftNeighbour.Value += Left.Value;
            }

            Node rightNeighbour = FindRightNeighbour();
            if (rightNeighbour != null && rightNeighbour.IsLeaf)
            {
                rightNeighbour.Value += Right.Value;
            }

            Value = 0;
            IsLeaf = true;
            Right = null;
            Left = null;
        }

        public Node FindLeftNeighbour()
        {
            Node current = this;
            while (true)
            {
                if (current.Parent == null)
                {
                    return null;
                }

                bool fromLeft = current.Parent.Left == current;
                if (fromLeft)
                {
                    current = current.Parent;
                }
                else
                {
                    current = current.Parent.Left;
                    break;
                }
            }

            while (true)
            {
                if (current.IsLeaf)
                {
                    return current;
                }

                current = current.Right;
            }
        }

        public Node FindRightNeighbour()
        {
            Node current = this;
            while (true)
            {
                if (current.Parent == null)
                {
                    return null;
                }

                bool fromRight = current.Parent.Right == current;
                if (fromRight)
                {
                    current = current.Parent;
                }
                else
                {
                    current = current.Parent.Right;
                    break;
                }
            }

            while (true)
            {
                if (current.IsLeaf)
                {
                    return current;
                }

                current = current.Left;
            }
        }

        public void Split()
        {
            if (!IsLeaf)
            {
                return;
            }

            Left = new Node((int)Value / 2, this);
            Right = new Node((int)Value / 2 + (int)Value % 2, this);
            Value = null;
            IsLeaf = false;
        }

        public Node FindExplodable()
        {
            if (IsLeaf)
            {
                return null;
            }

            if (Depth >= 4)
            {
                return this;
            }

            Node fromLeft = Left.FindExplodable();
            if (fromLeft != null)
            {
                return fromLeft;
            }

            Node fromRight = Right.FindExplodable();
            if (fromRight != null)
            {
                return fromRight;
            }

            return null;
        }

        public Node FindSplitable()
        {
            if (IsLeaf && Value > 9)
            {
                return this;
            }

            Node fromLeft = Left?.FindSplitable();
            if (fromLeft != null)
            {
                return fromLeft;
            }

            Node fromRight = Right?.FindSplitable();
            if (fromRight != null)
            {
                return fromRight;
            }

            return null;
        }

        public int Magnitude()
        {
            if (IsLeaf)
            {
                return (int)Value;
            }

            return Left.Magnitude() * 3 + Right.Magnitude() * 2;
        }

        public override string ToString()
        {
            if (IsLeaf)
            {
                return $"{Value}";
            }

            return $"[{Left},{Right}]";
        }
    }
}