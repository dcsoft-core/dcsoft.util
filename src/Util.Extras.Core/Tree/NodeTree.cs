using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;

// ReSharper disable StaticMemberInGenericType
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Local
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
// ReSharper disable NotAccessedField.Local

[assembly:
    SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Scope = "member",
        Target = "Common.NodeTree`1+EnumeratorBase`1.Count", MessageId = "o")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Scope = "member",
        Target = "Common.NodeTree`1+BaseEnumerableCollectionPair+BaseNodesEnumerableCollection.Count", MessageId = "o")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Scope = "member",
        Target = "Common.NodeTree`1.GetNodeTree(Common.INode`1<T>):Common.NodeTree`1<T>")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Scope = "member",
        Target = "Common.NodeTree`1.GetNodeTree(Common.ITree`1<T>):Common.NodeTree`1<T>")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Scope = "member",
        Target = "Common.NodeTree`1.NewTree():Common.ITree`1<T>")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Scope = "member",
        Target = "Common.NodeTree`1.NewNode():Common.INode`1<T>")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Scope = "member",
        Target = "Common.NodeTree`1.XmlAdapterTag")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Scope = "member",
        Target = "Common.NodeTree`1.XmlDeserialize(System.IO.Stream):Common.ITree`1<T>")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Common.IEnumerableCollectionPair`1.Nodes")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Common.NodeTree`1.Nodes")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Common.NodeTree`1+AllEnumerator.Nodes")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Common.NodeTree`1+BaseEnumerableCollectionPair.Nodes")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "Common.NodeTree`1+BaseEnumerableCollectionPair+BaseNodesEnumerableCollection.GetEnumerator():System.Collections.Generic.IEnumerator`1<Common.INode`1<T>>")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type",
        Target = "Common.NodeTree`1+AllEnumerator")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type",
        Target = "Common.NodeTree`1+BaseEnumerableCollectionPair")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type",
        Target = "Common.NodeTree`1+RootObject")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type",
        Target = "Common.NodeTree`1+NodeXmlSerializationAdapter")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type",
        Target = "Common.NodeTree`1+NodeXmlSerializationAdapter+IXmlCollection")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type",
        Target = "Common.NodeTree`1+TreeXmlSerializationAdapter")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Common.NodeTree`1.Dispose():System.Void")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Common.NodeTree`1.Finalize():System.Void")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Common.NodeTree`1+BaseEnumerableCollectionPair+BaseNodesEnumerableCollection.Dispose():System.Void")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Common.NodeTree`1+BaseEnumerableCollectionPair+BaseNodesEnumerableCollection.Finalize():System.Void")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Scope = "member",
        Target = "Common.NodeTree`1.XmlAdapterTag")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member",
        Target = "Common.NodeTree`1+NodeXmlSerializationAdapter.Children")]

namespace Util.Extras.Tree
{
    /// <summary>
    /// NodeTree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class NodeTree<T> : INode<T>, ITree<T>, ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        private T _data;

        /// <summary>
        /// 
        /// </summary>
        private NodeTree<T> _parent;

        /// <summary>
        /// 
        /// </summary>
        private NodeTree<T> _previous;

        /// <summary>
        /// 
        /// </summary>
        private NodeTree<T> _next;

        /// <summary>
        /// 
        /// </summary>
        private NodeTree<T> _child;

        /// <summary>
        /// 
        /// </summary>
        protected NodeTree()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        ~NodeTree()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventHandlerList?.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITree<T> NewTree()
        {
            return new RootObject();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataComparer"></param>
        /// <returns></returns>
        public static ITree<T> NewTree(IEqualityComparer<T> dataComparer)
        {
            return new RootObject(dataComparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static INode<T> NewNode()
        {
            return new NodeTree<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual NodeTree<T> CreateTree()
        {
            return new RootObject();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual NodeTree<T> CreateNode()
        {
            return new NodeTree<T>();
        }

        /// <summary>
        /// Obtains the <see cref="String"/> representation of this instance.
        /// </summary>
        /// <returns>The <see cref="String"/> representation of this instance.</returns>
        /// <remarks>
        /// <p>
        /// This method returns a <see cref="String"/> that represents this instance.
        /// </p>
        /// </remarks>
        public override string ToString()
        {
            var data = Data;
            return data == null ? string.Empty : data.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ToStringRecursive()
        {
            return All.Nodes.Cast<NodeTree<T>>().Aggregate(string.Empty,
                (current, node) => current + (new string('\t', node.Depth) + node + Environment.NewLine));
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Depth
        {
            get
            {
                var i = -1;
                for (INode<T> node = this; !node.IsRoot; node = node.Parent) i++;
                return i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int BranchIndex
        {
            get
            {
                var i = -1;
                for (INode<T> node = this; node != null; node = node.Previous) i++;
                return i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int BranchCount
        {
            get
            {
                var i = 0;
                for (var node = First; node != null; node = node.Next) i++;
                return i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual T DeepCopyData(T data)
        {
            //if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

            if (data == null)
            {
                Debug.Assert(true);
                return default(T);
            }

            // IDeepCopy
            if (data is IDeepCopy deepCopy) return (T)deepCopy.CreateDeepCopy();

            // ICloneable
            if (data is ICloneable cloneable) return (T)cloneable.Clone();

            // Copy constructor
            var ctor = data.GetType().GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null, new[] { typeof(T) }, null);
            if (ctor != null) return (T)ctor.Invoke(new object[] { data });
            //return ( T ) Activator.CreateInstance( typeof( T ), new object[] { data } );

            // give up
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        protected class RootObject : NodeTree<T>
        {
            private int _version;

            /// <summary>
            /// 
            /// </summary>
            protected override int Version
            {
                get => _version;
                set => _version = value;
            }

            private IEqualityComparer<T> _dataComparer;

            /// <summary>
            /// 
            /// </summary>
            public override IEqualityComparer<T> DataComparer
            {
                get => _dataComparer ??= EqualityComparer<T>.Default;
                set => _dataComparer = value;
            }

            /// <summary>
            /// 
            /// </summary>
            public RootObject()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dataComparer"></param>
            public RootObject(IEqualityComparer<T> dataComparer)
            {
                _dataComparer = dataComparer;
            }

            /// <inheritdoc />
            /// <summary>
            /// Obtains the <see cref="T:System.String" /> representation of this instance.
            /// </summary>
            /// <returns>The <see cref="T:System.String" /> representation of this instance.</returns>
            /// <remarks>
            /// <p>
            /// This method returns a <see cref="T:System.String" /> that represents this instance.
            /// </p>
            /// </remarks>
            public override string ToString()
            {
                return "ROOT: " + DataType.Name;
            }

            // Save
            /// <inheritdoc />
            /// <summary>
            /// Populates a SerializationInfo with the data needed to serialize the target T.
            /// </summary>
            /// <param name="info">The SerializationInfo to populate with data.</param>
            /// <param name="context">The destination for this serialization.</param>
            /// <remarks>
            /// <p>This method is called during serialization.</p>
            /// <p>Do not call this method directly.</p>
            /// </remarks>
#pragma warning disable SYSLIB0003 // 类型或成员已过时
            [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
#pragma warning restore SYSLIB0003 // 类型或成员已过时
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);

                info.AddValue("RootObjectVersion", 1);
                //info.AddValue( "DataType", _DataType );
            }

            // Load
            /// <inheritdoc />
            /// <summary>
            /// Initializes a new instance of the class during deserialization.
            /// </summary>
            /// <param name="info">The SerializationInfo populated with data.</param>
            /// <param name="context">The source for this serialization.</param>
            /// <remarks>
            /// <p>This method is called during deserialization.</p>
            /// <p>Do not call this method directly.</p>
            /// </remarks>
            protected RootObject(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                var iVersion = info.GetInt32("RootObjectVersion");
                if (iVersion != 1) throw new SerializationException("Unknown version");

                //_DataType = info.GetValue( "DataType", typeof( Type ) ) as Type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual RootObject GetRootObject => (RootObject)Root;

        /// <summary>
        /// 
        /// </summary>
        public virtual IEqualityComparer<T> DataComparer
        {
            get
            {
                if (!Root.IsTree) throw new InvalidOperationException("This is not a Tree");
                return GetRootObject.DataComparer;
            }

            set
            {
                if (!Root.IsTree) throw new InvalidOperationException("This is not a Tree");
                GetRootObject.DataComparer = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual int Version
        {
            get
            {
                var root = Root;
                if (!root.IsTree) throw new InvalidOperationException("This is not a Tree");
                return GetNodeTree(root).Version;
            }

            set
            {
                var root = Root;
                if (!root.IsTree) throw new InvalidOperationException("This is not a Tree");
                GetNodeTree(root).Version = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        protected bool HasChanged(int version)
        {
            return (Version != version);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void IncrementVersion()
        {
            var root = Root;
            if (!root.IsTree) throw new InvalidOperationException("This is not a Tree");
            GetNodeTree(root).Version++;
        }

        /// <inheritdoc />
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target T.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
        /// <remarks>
        /// <p>This method is called during serialization.</p>
        /// <p>Do not call this method directly.</p>
        /// </remarks>
#pragma warning disable SYSLIB0003 // 类型或成员已过时
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
#pragma warning restore SYSLIB0003 // 类型或成员已过时
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NodeTreeVersion", 1);
            info.AddValue("Data", _data);
            info.AddValue("Parent", _parent);
            info.AddValue("Previous", _previous);
            info.AddValue("Next", _next);
            info.AddValue("Child", _child);
        }

        /// <summary>
        /// Initializes a new instance of the class during deserialization.
        /// </summary>
        /// <param name="info">The SerializationInfo populated with data.</param>
        /// <param name="context">The source for this serialization.</param>
        /// <remarks>
        /// <p>This method is called during deserialization.</p>
        /// <p>Do not call this method directly.</p>
        /// </remarks>
        protected NodeTree(SerializationInfo info, StreamingContext context)
        {
            var iVersion = info.GetInt32("NodeTreeVersion");
            if (iVersion != 1) throw new SerializationException("Unknown version");

            _data = (T)info.GetValue("Data", typeof(T));
            _parent = (NodeTree<T>)info.GetValue("Parent", typeof(NodeTree<T>));
            _previous = (NodeTree<T>)info.GetValue("Previous", typeof(NodeTree<T>));
            _next = (NodeTree<T>)info.GetValue("Next", typeof(NodeTree<T>));
            _child = (NodeTree<T>)info.GetValue("Child", typeof(NodeTree<T>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public virtual void XmlSerialize(Stream stream)
        {
            XmlSerializer xmlSerializer;

            try
            {
                xmlSerializer = new XmlSerializer(typeof(TreeXmlSerializationAdapter));
            }
            catch (Exception)
            {
                Debug.Assert(false); // false
                throw;
            }

            try
            {
                xmlSerializer.Serialize(stream, new TreeXmlSerializationAdapter(XmlAdapterTag, this));
            }
            catch (Exception)
            {
                Debug.Assert(false); // false
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static ITree<T> XmlDeserialize(Stream stream)
        {
            XmlSerializer xmlSerializer;

            try
            {
                xmlSerializer = new XmlSerializer(typeof(TreeXmlSerializationAdapter));
            }
            catch (Exception)
            {
                Debug.Assert(false); // false
                throw;
            }

            object o;

            try
            {
                o = xmlSerializer.Deserialize(stream);
            }
            catch (Exception)
            {
                Debug.Assert(false); // false
                throw;
            }

            var adapter = (TreeXmlSerializationAdapter)o;

            var tree = adapter?.Tree;

            return tree;
        }

        /// <summary>
        /// XmlAdapterTag
        /// </summary>
        protected static readonly object XmlAdapterTag = new();

        /// <summary>
        /// 
        /// </summary>
        [XmlType("Tree")]
        public class TreeXmlSerializationAdapter
        {
#pragma warning disable IDE0052
            private int _version = 0;
#pragma warning restore IDE0052

            /// <summary>
            /// 
            /// </summary>
            [XmlAttribute]
            public int Version
            {
                get => 1;
                set => _version = value;
            }

            /// <summary>
            /// 
            /// </summary>
            [XmlIgnore]
            public ITree<T> Tree { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            private TreeXmlSerializationAdapter()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="tag"></param>
            /// <param name="tree"></param>
            public TreeXmlSerializationAdapter(object tag, ITree<T> tree)
            {
                if (!ReferenceEquals(XmlAdapterTag, tag)) throw new InvalidOperationException("Don't use this class");

                Tree = tree;
            }

            /// <summary>
            /// 
            /// </summary>
            public NodeXmlSerializationAdapter Root
            {
                get => new(XmlAdapterTag, Tree.Root);

                set
                {
                    Tree = NewTree();
                    ReformTree(Tree.Root, value);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="node"></param>
            private static void ReformTree(INode<T> parent, NodeXmlSerializationAdapter node)
            {
                foreach (NodeXmlSerializationAdapter child in node.Children)
                {
                    var n = parent.AddChild(child.Data);
                    ReformTree(n, child);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlType("Node")]
        public class NodeXmlSerializationAdapter
        {
            private int _version = 0;

            /// <summary>
            /// 
            /// </summary>
            [XmlAttribute]
            public int Version
            {
                get => 1;
                set => _version = value;
            }

            private readonly IXmlCollection _children = new ChildCollection();

            /// <summary>
            /// 
            /// </summary>
            [XmlIgnore]
            public INode<T> Node { get; }

            /// <summary>
            /// Load
            /// </summary>
            private NodeXmlSerializationAdapter()
            {
                Node = NewNode();
            }

            /// <summary>
            /// Save
            /// </summary>
            /// <param name="tag"></param>
            /// <param name="node"></param>
            public NodeXmlSerializationAdapter(object tag, INode<T> node)
            {
                if (!ReferenceEquals(XmlAdapterTag, tag)) throw new InvalidOperationException("Don't use this class");

                Node = node;

                foreach (var child in node.DirectChildren.Nodes)
                    _children.Add(new NodeXmlSerializationAdapter(XmlAdapterTag, child));
            }

            /// <summary>
            /// 
            /// </summary>
            public T Data
            {
                get => Node.Data;

                set => GetNodeTree(Node)._data = value;
            }

            /// <summary>
            /// 
            /// </summary>
            public IXmlCollection Children
            {
                get => _children;
                set => Debug.Assert(value == null);
            }

            /// <summary>
            /// 
            /// </summary>
            public interface IXmlCollection : ICollection
            {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="index"></param>
                /// <returns></returns>
                NodeXmlSerializationAdapter this[int index] { get; }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="item"></param>
                void Add(NodeXmlSerializationAdapter item);
            }

            private class ChildCollection : List<NodeXmlSerializationAdapter>, IXmlCollection
            {
            }
        }


        /// <summary>
        /// INode
        /// </summary>
        public T Data
        {
            get => _data;

            set
            {
                if (IsRoot) throw new InvalidOperationException("This is a Root");

                OnSetting(this, value);

                _data = value;

                OnSetDone(this, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Parent => _parent;

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Previous => _previous;

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Next => _next;

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Child => _child;

        /// <summary>
        /// 
        /// </summary>
        public ITree<T> Tree => (ITree<T>)Root;

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Root
        {
            get
            {
                INode<T> node = this;

                while (node.Parent != null)
                    node = node.Parent;

                return node;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Top
        {
            get
            {
                if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");
                //if ( this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
                if (IsRoot) return null;

                INode<T> node = this;

                while (node.Parent.Parent != null)
                    node = node.Parent;

                return node;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> First
        {
            get
            {
                INode<T> node = this;

                while (node.Previous != null)
                    node = node.Previous;

                return node;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Last
        {
            get
            {
                INode<T> node = this;

                while (node.Next != null)
                    node = node.Next;

                return node;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> LastChild => Child?.Last;

        /// <summary>
        /// 
        /// </summary>
        public bool HasPrevious => Previous != null;

        /// <summary>
        /// 
        /// </summary>
        public bool HasNext => Next != null;

        /// <summary>
        /// 
        /// </summary>
        public bool HasChild => Child != null;

        /// <summary>
        /// 
        /// </summary>
        public bool IsFirst => Previous == null;

        /// <summary>
        /// 
        /// </summary>
        public bool IsLast => Next == null;

        /// <summary>
        /// 
        /// </summary>
        public bool IsTree
        {
            get
            {
                if (!IsRoot) return false;
                return this is RootObject;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRoot
        {
            get
            {
                var b = (Parent == null);

                if (!b) return false;
                Debug.Assert(Previous == null);
                Debug.Assert(Next == null);

                return b;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasParent
        {
            get
            {
                //if ( IsRoot ) throw new InvalidOperationException( "This is a Root" );
                if (IsRoot) return false;
                return Parent.Parent != null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTop
        {
            get
            {
                //if ( IsRoot ) throw new InvalidOperationException( "This is a Root" );
                if (IsRoot) return false;
                return Parent.Parent == null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLeaf => !HasChild;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual INode<T> this[T item]
        {
            get
            {
                if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

                var comparer = DataComparer;

                return All.Nodes.FirstOrDefault(n => comparer.Equals(n.Data, item));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Contains(INode<T> item)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            return All.Nodes.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Contains(T item)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            return All.Values.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public INode<T> InsertPrevious(T o)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var newNode = CreateNode();
            newNode._data = o;

            InsertPreviousCore(newNode);

            return newNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public INode<T> InsertNext(T o)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var newNode = CreateNode();
            newNode._data = o;

            InsertNextCore(newNode);

            return newNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public INode<T> InsertChild(T o)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var newNode = CreateNode();
            newNode._data = o;

            InsertChildCore(newNode);

            return newNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public INode<T> Add(T o)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            return Last.InsertNext(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public INode<T> AddChild(T o)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            return Child == null ? InsertChild(o) : Child.Add(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        public void InsertPrevious(ITree<T> tree)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var newTree = GetNodeTree(tree);

            if (!newTree.IsRoot) throw new ArgumentException("Tree is not a Root");
            if (!newTree.IsTree) throw new ArgumentException("Tree is not a tree");

            for (var n = newTree.Child; n != null; n = n.Next)
            {
                var node = GetNodeTree(n);
                var copy = node.CopyCore();
                InsertPreviousCore(copy);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        public void InsertNext(ITree<T> tree)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var newTree = GetNodeTree(tree);

            if (!newTree.IsRoot) throw new ArgumentException("Tree is not a Root");
            if (!newTree.IsTree) throw new ArgumentException("Tree is not a tree");

            for (var n = newTree.LastChild; n != null; n = n.Previous)
            {
                var node = GetNodeTree(n);
                var copy = node.CopyCore();
                InsertNextCore(copy);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        public void InsertChild(ITree<T> tree)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var newTree = GetNodeTree(tree);

            if (!newTree.IsRoot) throw new ArgumentException("Tree is not a Root");
            if (!newTree.IsTree) throw new ArgumentException("Tree is not a tree");

            for (var n = newTree.LastChild; n != null; n = n.Previous)
            {
                var node = GetNodeTree(n);
                var copy = node.CopyCore();
                InsertChildCore(copy);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        public void Add(ITree<T> tree)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            Last.InsertNext(tree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        public void AddChild(ITree<T> tree)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            if (Child == null)
                InsertChild(tree);
            else
                Child.Add(tree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newINode"></param>
        protected virtual void InsertPreviousCore(INode<T> newINode)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!newINode.IsRoot) throw new ArgumentException("Node is not a Root");
            if (newINode.IsTree) throw new ArgumentException("Node is a tree");

            IncrementVersion();

            OnInserting(this, NodeTreeInsertOperation.Previous, newINode);

            var newNode = GetNodeTree(newINode);

            newNode._parent = _parent;
            newNode._previous = _previous;
            newNode._next = this;
            _previous = newNode;

            if (newNode.Previous != null)
            {
                var previous = GetNodeTree(newNode.Previous);
                previous._next = newNode;
            }
            else // this is a first node
            {
                var parent = GetNodeTree(newNode.Parent);
                parent._child = newNode;
            }

            OnInserted(this, NodeTreeInsertOperation.Previous, newINode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newINode"></param>
        protected virtual void InsertNextCore(INode<T> newINode)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!newINode.IsRoot) throw new ArgumentException("Node is not a Root");
            if (newINode.IsTree) throw new ArgumentException("Node is a tree");

            IncrementVersion();

            OnInserting(this, NodeTreeInsertOperation.Next, newINode);

            var newNode = GetNodeTree(newINode);

            newNode._parent = _parent;
            newNode._previous = this;
            newNode._next = _next;
            _next = newNode;

            if (newNode.Next != null)
            {
                var next = GetNodeTree(newNode.Next);
                next._previous = newNode;
            }

            OnInserted(this, NodeTreeInsertOperation.Next, newINode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newINode"></param>
        protected virtual void InsertChildCore(INode<T> newINode)
        {
            if (!newINode.IsRoot) throw new ArgumentException("Node is not a Root");
            if (newINode.IsTree) throw new ArgumentException("Node is a tree");

            IncrementVersion();

            OnInserting(this, NodeTreeInsertOperation.Child, newINode);

            var newNode = GetNodeTree(newINode);

            newNode._parent = this;
            newNode._next = _child;
            _child = newNode;

            if (newNode.Next != null)
            {
                var next = GetNodeTree(newNode.Next);
                next._previous = newNode;
            }

            OnInserted(this, NodeTreeInsertOperation.Child, newINode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newINode"></param>
        protected virtual void AddCore(INode<T> newINode)
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");

            var lastNode = GetNodeTree(Last);

            lastNode.InsertNextCore(newINode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newINode"></param>
        protected virtual void AddChildCore(INode<T> newINode)
        {
            if (Child == null)
                InsertChildCore(newINode);
            else
            {
                var childNode = GetNodeTree(Child);

                childNode.AddCore(newINode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public ITree<T> Cut(T o)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var n = this[o];
            return n?.Cut();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public ITree<T> Copy(T o)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var n = this[o];
            return n?.Copy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public ITree<T> DeepCopy(T o)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var n = this[o];
            return n?.DeepCopy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Remove(T o)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var n = this[o];
            if (n == null) return false;

            n.Remove();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private NodeTree<T> BoxInTree(INode<T> node)
        {
            if (!node.IsRoot) throw new ArgumentException("Node is not a Root");
            if (node.IsTree) throw new ArgumentException("Node is a tree");

            var tree = CreateTree();

            tree.AddChildCore(node);

            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITree<T> Cut()
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            var node = CutCore();

            return BoxInTree(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITree<T> Copy()
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            if (IsTree)
            {
                var newTree = CopyCore();

                return newTree;
            }
            else
            {
                var newNode = CopyCore();

                return BoxInTree(newNode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITree<T> DeepCopy()
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            if (IsTree)
            {
                var newTree = DeepCopyCore();

                return newTree;
            }
            else
            {
                var newNode = DeepCopyCore();

                return BoxInTree(newNode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Remove()
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");

            RemoveCore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual NodeTree<T> CutCore()
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");

            IncrementVersion();

            OnCutting(this);

            var oldRoot = Root;

            if (_next != null)
                _next._previous = _previous;

            if (Previous != null)
                _previous._next = _next;
            else // this is a first node
            {
                Debug.Assert(Parent.Child == this);
                _parent._child = _next;
                Debug.Assert(Next?.Previous == null);
            }

            _parent = null;
            _previous = null;
            _next = null;

            OnCutDone(oldRoot, this);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual NodeTree<T> CopyCore()
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");
            if (IsRoot && !IsTree) throw new InvalidOperationException("This is a Root");

            if (IsTree)
            {
                var newTree = CreateTree();

                OnCopying(this, newTree);

                CopyChildNodes(this, newTree, false);

                OnCopied(this, newTree);

                return newTree;
            }

            var newNode = CreateNode();

            newNode._data = Data;

            OnCopying(this, newNode);

            CopyChildNodes(this, newNode, false);

            OnCopied(this, newNode);

            return newNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual NodeTree<T> DeepCopyCore()
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");
            if (IsRoot && !IsTree) throw new InvalidOperationException("This is a Root");

            if (IsTree)
            {
                var newTree = CreateTree();

                OnCopying(this, newTree);

                CopyChildNodes(this, newTree, true);

                OnCopied(this, newTree);

                return newTree;
            }

            var newNode = CreateNode();

            newNode._data = DeepCopyData(Data);

            OnDeepCopying(this, newNode);

            CopyChildNodes(this, newNode, true);

            OnDeepCopied(this, newNode);

            return newNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        /// <param name="bDeepCopy"></param>
        private void CopyChildNodes(INode<T> oldNode, NodeTree<T> newNode, bool bDeepCopy)
        {
            NodeTree<T> previousNewChildNode = null;

            for (var oldChildNode = oldNode.Child; oldChildNode != null; oldChildNode = oldChildNode.Next)
            {
                var newChildNode = CreateNode();

                newChildNode._data = !bDeepCopy ? oldChildNode.Data : DeepCopyData(oldChildNode.Data);

                //				if ( ! bDeepCopy )
                //					OnCopying( oldChildNode, newChildNode );
                //				else
                //					OnDeepCopying( oldChildNode, newChildNode );

                if (oldChildNode.Previous == null) newNode._child = newChildNode;

                newChildNode._parent = newNode;
                newChildNode._previous = previousNewChildNode;
                if (previousNewChildNode != null) previousNewChildNode._next = newChildNode;

                //				if ( ! bDeepCopy )
                //					OnCopied( oldChildNode, newChildNode );
                //				else
                //					OnDeepCopied( oldChildNode, newChildNode );

                CopyChildNodes(oldChildNode, newChildNode, bDeepCopy);

                previousNewChildNode = newChildNode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void RemoveCore()
        {
            if (IsRoot) throw new InvalidOperationException("This is a Root");

            CutCore();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanMoveToParent
        {
            get
            {
                if (IsRoot) return false;
                return !IsTop;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanMoveToPrevious
        {
            get
            {
                if (IsRoot) return false;
                return !IsFirst;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanMoveToNext
        {
            get
            {
                if (IsRoot) return false;
                return !IsLast;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanMoveToChild
        {
            get
            {
                if (IsRoot) return false;
                return !IsFirst;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanMoveToFirst
        {
            get
            {
                if (IsRoot) return false;
                return !IsFirst;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanMoveToLast
        {
            get
            {
                if (IsRoot) return false;
                return !IsLast;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToParent()
        {
            if (!CanMoveToParent) throw new InvalidOperationException("Cannot move to Parent");

            var parentNode = GetNodeTree(Parent);

            var thisNode = CutCore();

            parentNode.InsertNextCore(thisNode);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToPrevious()
        {
            if (!CanMoveToPrevious) throw new InvalidOperationException("Cannot move to Previous");

            var previousNode = GetNodeTree(Previous);

            var thisNode = CutCore();

            previousNode.InsertPreviousCore(thisNode);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToNext()
        {
            if (!CanMoveToNext) throw new InvalidOperationException("Cannot move to Next");

            var nextNode = GetNodeTree(Next);

            var thisNode = CutCore();

            nextNode.InsertNextCore(thisNode);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToChild()
        {
            if (!CanMoveToChild) throw new InvalidOperationException("Cannot move to Child");

            var previousNode = GetNodeTree(Previous);

            var thisNode = CutCore();

            previousNode.AddChildCore(thisNode);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToFirst()
        {
            if (!CanMoveToFirst) throw new InvalidOperationException("Cannot move to first");

            var firstNode = GetNodeTree(First);

            var thisNode = CutCore();

            firstNode.InsertPreviousCore(thisNode);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToLast()
        {
            if (!CanMoveToLast) throw new InvalidOperationException("Cannot move to last");

            var lastNode = GetNodeTree(Last);

            var thisNode = CutCore();

            lastNode.InsertNextCore(thisNode);
        }

        //-----------------------------------------------------------------------------
        // Enumerators

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerableCollection<INode<T>> Nodes => All.Nodes;

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerableCollection<T> Values => All.Values;

        //-----------------------------------------------------------------------------
        // BaseEnumerableCollectionPair

        /// <summary>
        /// 
        /// </summary>
        protected abstract class BaseEnumerableCollectionPair : IEnumerableCollectionPair<T>
        {
            /// <summary>
            /// 
            /// </summary>
            protected NodeTree<T> Root { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="root"></param>
            protected BaseEnumerableCollectionPair(NodeTree<T> root)
            {
                Root = root;
            }

            /// <summary>
            /// Nodes
            /// </summary>
            public abstract IEnumerableCollection<INode<T>> Nodes { get; }

            /// <summary>
            /// 
            /// </summary>
            protected abstract class BaseNodesEnumerableCollection : IEnumerableCollection<INode<T>>,
                IEnumerator<INode<T>>
            {
                private readonly int _version = 0;

                /// <summary>
                /// 
                /// </summary>
                protected NodeTree<T> Root { get; set; }

                /// <summary>
                /// 
                /// </summary>
                protected INode<T> CurrentNode { get; set; }

                /// <summary>
                /// 
                /// </summary>
                protected bool BeforeFirst { get; set; }

                /// <summary>
                /// 
                /// </summary>
                protected bool AfterLast { get; set; }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="root"></param>
                protected BaseNodesEnumerableCollection(NodeTree<T> root)
                {
                    Root = root;
                    CurrentNode = root;

                    _version = Root.Version;
                }

                /// <summary>
                /// 
                /// </summary>
                ~BaseNodesEnumerableCollection()
                {
                    Dispose(false);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                protected abstract BaseNodesEnumerableCollection CreateCopy();

                /// <summary>
                /// 
                /// </summary>
                protected virtual bool HasChanged => Root.HasChanged(_version);

                /// <summary>
                /// IDisposable
                /// </summary>
                public void Dispose()
                {
                    Dispose(true);

                    GC.SuppressFinalize(this);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="disposing"></param>
                protected virtual void Dispose(bool disposing)
                {
                }

                /// <summary>
                /// IEnumerable
                /// </summary>
                /// <returns></returns>
                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                /// <summary>
                /// IEnumerable
                /// </summary>
                /// <returns></returns>
                public virtual IEnumerator<INode<T>> GetEnumerator()
                {
                    return this;
                }

                /// <summary>
                /// ICollection
                /// </summary>
                public virtual int Count
                {
                    get
                    {
                        var e = CreateCopy();
                        return e.Count();
                    }
                }

                /// <summary>
                /// 
                /// </summary>
                public virtual bool IsSynchronized => false;

                /// <summary>
                /// 
                /// </summary>
                public virtual object SyncRoot { get; } = new();

                void ICollection.CopyTo(Array array, int index)
                {
                    if (array == null) throw new ArgumentNullException(nameof(array));
                    if (array.Rank > 1) throw new ArgumentException(@"array is multidimensional", nameof(array));
                    if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

                    var count = Count;

                    if (count > 0)
                        if (index >= array.Length)
                            throw new ArgumentException(@"index is out of bounds", nameof(index));

                    if (index + count > array.Length)
                        throw new ArgumentException(@"Not enough space in array", nameof(array));

                    var e = CreateCopy();

                    foreach (var n in e)
                        array.SetValue(n, index++);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="array"></param>
                /// <param name="index"></param>
                public virtual void CopyTo(T[] array, int index)
                {
                    ((ICollection)this).CopyTo(array, index);
                }

                /// <summary>
                /// ICollectionEnumerable
                /// </summary>
                /// <param name="item"></param>
                /// <returns></returns>
                public virtual bool Contains(INode<T> item)
                {
                    var e = CreateCopy();

                    IEqualityComparer<INode<T>> comparer = EqualityComparer<INode<T>>.Default;

                    return e.Any(n => comparer.Equals(n, item));
                }

                /// <summary>
                /// IEnumerator
                /// </summary>
                object IEnumerator.Current => Current;

                /// <summary>
                /// IEnumerator
                /// </summary>
                public virtual void Reset()
                {
                    if (HasChanged) throw new InvalidOperationException("Tree has been modified.");

                    CurrentNode = Root;

                    BeforeFirst = true;
                    AfterLast = false;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public virtual bool MoveNext()
                {
                    if (HasChanged) throw new InvalidOperationException("Tree has been modified.");

                    BeforeFirst = false;

                    return true;
                }

                /// <summary>
                /// 
                /// </summary>
                public virtual INode<T> Current
                {
                    get
                    {
                        if (BeforeFirst) throw new InvalidOperationException("Enumeration has not started.");
                        if (AfterLast) throw new InvalidOperationException("Enumeration has finished.");

                        return CurrentNode;
                    }
                }
            }

            /// <summary>
            /// Values
            /// </summary>
            public virtual IEnumerableCollection<T> Values => new ValuesEnumerableCollection(Root.DataComparer, Nodes);

            private sealed class ValuesEnumerableCollection : IEnumerableCollection<T>, IEnumerator<T>
            {
                private readonly IEqualityComparer<T> _dataComparer;
                private readonly IEnumerableCollection<INode<T>> _nodes;
                private readonly IEnumerator<INode<T>> _enumerator;

                public ValuesEnumerableCollection(IEqualityComparer<T> dataComparer,
                    IEnumerableCollection<INode<T>> nodes)
                {
                    _dataComparer = dataComparer;
                    _nodes = nodes;
                    _enumerator = _nodes.GetEnumerator();
                }

                private ValuesEnumerableCollection(ValuesEnumerableCollection o)
                {
                    _nodes = o._nodes;
                    _enumerator = _nodes.GetEnumerator();
                }

                private ValuesEnumerableCollection CreateCopy()
                {
                    return new ValuesEnumerableCollection(this);
                }

                // IDisposable
                ~ValuesEnumerableCollection()
                {
                    Dispose(false);
                }

                public void Dispose()
                {
                    Dispose(true);

                    GC.SuppressFinalize(this);
                }

                private static void Dispose(bool disposing)
                {
                }

                // IEnumerable
                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                // IEnumerable<T>
                public IEnumerator<T> GetEnumerator()
                {
                    return this;
                }

                // ICollection
                public int Count => _nodes.Count;

                public bool IsSynchronized => false;

                public object SyncRoot => _nodes.SyncRoot;

                public void CopyTo(Array array, int index)
                {
                    if (array == null) throw new ArgumentNullException(nameof(array));
                    if (array.Rank > 1) throw new ArgumentException(@"array is multidimensional", nameof(array));
                    if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

                    var count = Count;

                    if (count > 0)
                        if (index >= array.Length)
                            throw new ArgumentException(@"index is out of bounds", nameof(index));

                    if (index + count > array.Length)
                        throw new ArgumentException(@"Not enough space in array", nameof(array));

                    var e = CreateCopy();

                    foreach (var n in e)
                        array.SetValue(n, index++);
                }

                // IEnumerableCollection<T>
                public bool Contains(T item)
                {
                    var e = CreateCopy();

                    return e.Any(n => _dataComparer.Equals(n, item));
                }

                // IEnumerator
                object IEnumerator.Current => Current;

                // IEnumerator<T> Members
                public void Reset()
                {
                    _enumerator.Reset();
                }

                public bool MoveNext()
                {
                    return _enumerator.MoveNext();
                }

                public T Current
                {
                    get
                    {
                        if (_enumerator == null)
                        {
                            Debug.Assert(false);
                        }

                        if (_enumerator.Current != null) return _enumerator.Current.Data;
                        Debug.Assert(false);
                        return default(T);
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------
        // AllEnumerator

        /// <summary>
        /// 
        /// </summary>
        public IEnumerableCollectionPair<T> All => new AllEnumerator(this);

        /// <summary>
        /// 
        /// </summary>
        protected class AllEnumerator : BaseEnumerableCollectionPair
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="root"></param>
            public AllEnumerator(NodeTree<T> root) : base(root)
            {
            }

            /// <summary>
            /// 
            /// </summary>
            public override IEnumerableCollection<INode<T>> Nodes => new NodesEnumerableCollection(Root);

            /// <summary>
            /// 
            /// </summary>
            protected class NodesEnumerableCollection : BaseNodesEnumerableCollection
            {
                private bool _first = true;

                /// <summary>
                /// 
                /// </summary>
                /// <param name="root"></param>
                public NodesEnumerableCollection(NodeTree<T> root) : base(root)
                {
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="o"></param>
                protected NodesEnumerableCollection(NodesEnumerableCollection o) : base(o.Root)
                {
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                protected override BaseNodesEnumerableCollection CreateCopy()
                {
                    return new NodesEnumerableCollection(this);
                }

                /// <summary>
                /// 
                /// </summary>
                public override void Reset()
                {
                    base.Reset();

                    _first = true;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public override bool MoveNext()
                {
                    if (!base.MoveNext()) goto hell;

                    if (CurrentNode == null) throw new InvalidOperationException("Current is null");

                    if (CurrentNode.IsRoot)
                    {
                        CurrentNode = CurrentNode.Child;
                        if (CurrentNode == null) goto hell;
                    }

                    if (_first)
                    {
                        _first = false;
                        return true;
                    }

                    if (CurrentNode.Child != null)
                    {
                        CurrentNode = CurrentNode.Child;
                        return true;
                    }

                    for (; CurrentNode.Parent != null; CurrentNode = CurrentNode.Parent)
                    {
                        if (CurrentNode == Root) goto hell;
                        if (CurrentNode.Next == null) continue;
                        CurrentNode = CurrentNode.Next;
                        return true;
                    }

                    hell:

                    AfterLast = true;
                    return false;
                }
            }
        }

        //-----------------------------------------------------------------------------
        // AllChildrenEnumerator

        /// <summary>
        /// 
        /// </summary>
        public IEnumerableCollectionPair<T> AllChildren => new AllChildrenEnumerator(this);

        private class AllChildrenEnumerator : BaseEnumerableCollectionPair
        {
            public AllChildrenEnumerator(NodeTree<T> root) : base(root)
            {
            }

            public override IEnumerableCollection<INode<T>> Nodes => new NodesEnumerableCollection(Root);

            private class NodesEnumerableCollection : BaseNodesEnumerableCollection
            {
                public NodesEnumerableCollection(NodeTree<T> root) : base(root)
                {
                }

                private NodesEnumerableCollection(NodesEnumerableCollection o) : base(o.Root)
                {
                }

                protected override BaseNodesEnumerableCollection CreateCopy()
                {
                    return new NodesEnumerableCollection(this);
                }

                public override bool MoveNext()
                {
                    if (!base.MoveNext()) goto hell;

                    if (CurrentNode == null) throw new InvalidOperationException("Current is null");

                    if (CurrentNode.Child != null)
                    {
                        CurrentNode = CurrentNode.Child;
                        return true;
                    }

                    for (; CurrentNode.Parent != null; CurrentNode = CurrentNode.Parent)
                    {
                        if (CurrentNode == Root) goto hell;
                        if (CurrentNode.Next == null) continue;
                        CurrentNode = CurrentNode.Next;
                        return true;
                    }

                    hell:

                    AfterLast = true;
                    return false;
                }
            }
        }

        //-----------------------------------------------------------------------------
        // DirectChildrenEnumerator

        /// <summary>
        /// 
        /// </summary>
        public IEnumerableCollectionPair<T> DirectChildren => new DirectChildrenEnumerator(this);

        private class DirectChildrenEnumerator : BaseEnumerableCollectionPair
        {
            public DirectChildrenEnumerator(NodeTree<T> root) : base(root)
            {
            }

            public override IEnumerableCollection<INode<T>> Nodes => new NodesEnumerableCollection(Root);

            private class NodesEnumerableCollection : BaseNodesEnumerableCollection
            {
                public NodesEnumerableCollection(NodeTree<T> root) : base(root)
                {
                }

                private NodesEnumerableCollection(NodesEnumerableCollection o) : base(o.Root)
                {
                }

                protected override BaseNodesEnumerableCollection CreateCopy()
                {
                    return new NodesEnumerableCollection(this);
                }

                public override int Count => Root.DirectChildCount;

                public override bool MoveNext()
                {
                    if (!base.MoveNext()) goto hell;

                    if (CurrentNode == null) throw new InvalidOperationException("Current is null");

                    CurrentNode = CurrentNode == Root ? Root.Child : CurrentNode.Next;

                    if (CurrentNode != null) return true;

                    hell:

                    AfterLast = true;
                    return false;
                }
            }
        }

        //-----------------------------------------------------------------------------
        // DirectChildrenInReverseEnumerator

        /// <summary>
        /// 
        /// </summary>
        public IEnumerableCollectionPair<T> DirectChildrenInReverse => new DirectChildrenInReverseEnumerator(this);

        private class DirectChildrenInReverseEnumerator : BaseEnumerableCollectionPair
        {
            public DirectChildrenInReverseEnumerator(NodeTree<T> root) : base(root)
            {
            }

            public override IEnumerableCollection<INode<T>> Nodes => new NodesEnumerableCollection(Root);

            private class NodesEnumerableCollection : BaseNodesEnumerableCollection
            {
                public NodesEnumerableCollection(NodeTree<T> root) : base(root)
                {
                }

                private NodesEnumerableCollection(NodesEnumerableCollection o) : base(o.Root)
                {
                }

                protected override BaseNodesEnumerableCollection CreateCopy()
                {
                    return new NodesEnumerableCollection(this);
                }

                public override int Count => Root.DirectChildCount;

                public override bool MoveNext()
                {
                    if (!base.MoveNext()) goto hell;

                    if (CurrentNode == null) throw new InvalidOperationException("Current is null");

                    CurrentNode = CurrentNode == Root ? Root.LastChild : CurrentNode.Previous;

                    if (CurrentNode != null) return true;

                    hell:

                    AfterLast = true;
                    return false;
                }
            }
        }

        //-----------------------------------------------------------------------------
        // DirectChildCount

        /// <summary>
        /// 
        /// </summary>
        public int DirectChildCount
        {
            get
            {
                var i = 0;

                for (var n = Child; n != null; n = n.Next) i++;

                return i;
            }
        }

        //-----------------------------------------------------------------------------
        // ITree<T>

        /// <summary>
        /// 
        /// </summary>
        public virtual Type DataType => typeof(T);

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            if (!IsRoot) throw new InvalidOperationException("This is not a Root");
            if (!IsTree) throw new InvalidOperationException("This is not a tree");

            OnClearing(this);

            _child = null;

            OnCleared(this);
        }

        //-----------------------------------------------------------------------------
        // GetNodeTree

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        protected static NodeTree<T> GetNodeTree(ITree<T> tree)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));

            return (NodeTree<T>)tree; // can throw an InvalidCastException.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected static NodeTree<T> GetNodeTree(INode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return (NodeTree<T>)node; // can throw an InvalidCastException.
        }

        //-----------------------------------------------------------------------------
        // ICollection

        //		public virtual bool IsSynchronized { get { return false; } } // Not thread safe

        //		public virtual T SyncRoot { get { return this; } } // Not sure about this!

        /// <summary>
        /// 
        /// </summary>
        public virtual int Count
        {
            get
            {
                var i = IsRoot ? 0 : 1;

                for (var n = Child; n != null; n = n.Next)
                    i += n.Count;

                return i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsReadOnly => false;

        //-----------------------------------------------------------------------------
        // Events

        /// <summary>
        /// 
        /// </summary>
        protected EventHandlerList EventHandlerList { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected EventHandlerList GetCreateEventHandlerList()
        {
            return EventHandlerList ??= new EventHandlerList();
        }

        /// <summary>
        /// 
        /// </summary>
        protected static object ValidateEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object ClearingEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object ClearedEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object SettingEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object SetDoneEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object InsertingEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object InsertedEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object CuttingEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object CutDoneEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object CopyingEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object CopiedEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object DeepCopyingEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        protected static object DeepCopiedEventKey { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeDataEventArgs<T>> Validate
        {
            add => GetCreateEventHandlerList().AddHandler(ValidateEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(ValidateEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Clearing
        {
            add => GetCreateEventHandlerList().AddHandler(ClearingEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(ClearingEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Cleared
        {
            add => GetCreateEventHandlerList().AddHandler(ClearedEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(ClearedEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeDataEventArgs<T>> Setting
        {
            add => GetCreateEventHandlerList().AddHandler(SettingEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(SettingEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeDataEventArgs<T>> SetDone
        {
            add => GetCreateEventHandlerList().AddHandler(SetDoneEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(SetDoneEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeInsertEventArgs<T>> Inserting
        {
            add => GetCreateEventHandlerList().AddHandler(InsertingEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(InsertingEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeInsertEventArgs<T>> Inserted
        {
            add => GetCreateEventHandlerList().AddHandler(InsertedEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(InsertedEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Cutting
        {
            add => GetCreateEventHandlerList().AddHandler(CuttingEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(CuttingEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CutDone
        {
            add => GetCreateEventHandlerList().AddHandler(CutDoneEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(CutDoneEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeNodeEventArgs<T>> Copying
        {
            add => GetCreateEventHandlerList().AddHandler(CopyingEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(CopyingEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeNodeEventArgs<T>> Copied
        {
            add => GetCreateEventHandlerList().AddHandler(CopiedEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(CopiedEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying
        {
            add => GetCreateEventHandlerList().AddHandler(DeepCopyingEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(DeepCopyingEventKey, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied
        {
            add => GetCreateEventHandlerList().AddHandler(DeepCopiedEventKey, value);

            remove => GetCreateEventHandlerList().RemoveHandler(DeepCopiedEventKey, value);
        }


        //-----------------------------------------------------------------------------
        // Validate

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        protected virtual void OnValidate(INode<T> node, T data)
        {
            if (!Root.IsTree) throw new InvalidOperationException("This is not a tree");
            if (data is INode<T>) throw new ArgumentException("Object is a node");

            if ((!typeof(T).IsClass) || data != null)
                if (!DataType.IsInstanceOfType(data))
                    throw new ArgumentException("Object is not a " + DataType.Name);

            var e = (EventHandler<NodeTreeDataEventArgs<T>>)EventHandlerList?[ValidateEventKey];
            e?.Invoke(node, new NodeTreeDataEventArgs<T>(data));

            if (!IsRoot) GetNodeTree(Root).OnValidate(node, data);
        }

        //-----------------------------------------------------------------------------
        // Clear

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        protected virtual void OnClearing(ITree<T> tree)
        {
            var e = (EventHandler)EventHandlerList?[ClearingEventKey];
            e?.Invoke(tree, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        protected virtual void OnCleared(ITree<T> tree)
        {
            var e = (EventHandler)EventHandlerList?[ClearedEventKey];
            e?.Invoke(tree, EventArgs.Empty);
        }

        //-----------------------------------------------------------------------------
        // Set

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        protected virtual void OnSetting(INode<T> node, T data)
        {
            OnSettingCore(node, data, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnSettingCore(INode<T> node, T data, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeDataEventArgs<T>>)EventHandlerList?[SettingEventKey];
            e?.Invoke(node, new NodeTreeDataEventArgs<T>(data));

            if (!IsRoot) GetNodeTree(Root).OnSettingCore(node, data, false);

            if (raiseValidate) OnValidate(node, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        protected virtual void OnSetDone(INode<T> node, T data)
        {
            OnSetDoneCore(node, data, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnSetDoneCore(INode<T> node, T data, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeDataEventArgs<T>>)EventHandlerList?[SetDoneEventKey];
            e?.Invoke(node, new NodeTreeDataEventArgs<T>(data));

            if (!IsRoot) GetNodeTree(Root).OnSetDoneCore(node, data, false);

            // if ( raiseValidate ) OnValidate( Node, Data );
        }

        //-----------------------------------------------------------------------------
        // Insert

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="operation"></param>
        /// <param name="newNode"></param>
        protected virtual void OnInserting(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode)
        {
            OnInsertingCore(oldNode, operation, newNode, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="operation"></param>
        /// <param name="newNode"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnInsertingCore(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode,
            bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeInsertEventArgs<T>>)EventHandlerList?[InsertingEventKey];
            e?.Invoke(oldNode, new NodeTreeInsertEventArgs<T>(operation, newNode));

            if (!IsRoot) GetNodeTree(Root).OnInsertingCore(oldNode, operation, newNode, false);

            if (raiseValidate) OnValidate(oldNode, newNode.Data);

            if (raiseValidate) OnInsertingTree(newNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        protected virtual void OnInsertingTree(INode<T> newNode)
        {
            for (var child = newNode.Child; child != null; child = child.Next)
            {
                OnInsertingTree(newNode, child);

                OnInsertingTree(child);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="child"></param>
        protected virtual void OnInsertingTree(INode<T> newNode, INode<T> child)
        {
            OnInsertingTreeCore(newNode, child, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="child"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnInsertingTreeCore(INode<T> newNode, INode<T> child, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeInsertEventArgs<T>>)EventHandlerList?[InsertingEventKey];
            e?.Invoke(newNode, new NodeTreeInsertEventArgs<T>(NodeTreeInsertOperation.Tree, child));

            if (!IsRoot) GetNodeTree(Root).OnInsertingTreeCore(newNode, child, false);

            if (raiseValidate) OnValidate(newNode, child.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="operation"></param>
        /// <param name="newNode"></param>
        protected virtual void OnInserted(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode)
        {
            OnInsertedCore(oldNode, operation, newNode, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="operation"></param>
        /// <param name="newNode"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnInsertedCore(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode,
            bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeInsertEventArgs<T>>)EventHandlerList?[InsertedEventKey];
            e?.Invoke(oldNode, new NodeTreeInsertEventArgs<T>(operation, newNode));

            if (!IsRoot) GetNodeTree(Root).OnInsertedCore(oldNode, operation, newNode, false);

            //if ( raiseValidate ) OnValidate( OldNode, NewNode.Data );

            if (raiseValidate) OnInsertedTree(newNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        protected virtual void OnInsertedTree(INode<T> newNode)
        {
            for (var child = newNode.Child; child != null; child = child.Next)
            {
                OnInsertedTree(newNode, child);

                OnInsertedTree(child);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="child"></param>
        protected virtual void OnInsertedTree(INode<T> newNode, INode<T> child)
        {
            OnInsertedTreeCore(newNode, child, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="child"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnInsertedTreeCore(INode<T> newNode, INode<T> child, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeInsertEventArgs<T>>)EventHandlerList?[InsertedEventKey];
            e?.Invoke(newNode, new NodeTreeInsertEventArgs<T>(NodeTreeInsertOperation.Tree, child));

            if (!IsRoot) GetNodeTree(Root).OnInsertedTreeCore(newNode, child, false);

            //if ( raiseValidate ) OnValidate( newNode, child.Data );
        }

        //-----------------------------------------------------------------------------
        // Cut

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        protected virtual void OnCutting(INode<T> oldNode)
        {
            var e = (EventHandler)EventHandlerList?[CuttingEventKey];
            e?.Invoke(oldNode, EventArgs.Empty);

            if (!IsRoot) GetNodeTree(Root).OnCutting(oldNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldRoot"></param>
        /// <param name="oldNode"></param>
        protected virtual void OnCutDone(INode<T> oldRoot, INode<T> oldNode)
        {
            var e = (EventHandler)EventHandlerList?[CutDoneEventKey];
            e?.Invoke(oldNode, EventArgs.Empty);

            if (!IsTree) GetNodeTree(oldRoot).OnCutDone(oldRoot, oldNode);
        }

        //-----------------------------------------------------------------------------
        // Copy

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        protected virtual void OnCopying(INode<T> oldNode, INode<T> newNode)
        {
            OnCopyingCore(oldNode, newNode, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnCopyingCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeNodeEventArgs<T>>)EventHandlerList?[CopyingEventKey];
            e?.Invoke(oldNode, new NodeTreeNodeEventArgs<T>(newNode));

            if (!IsRoot) GetNodeTree(Root).OnCopyingCore(oldNode, newNode, false);

            //if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        protected virtual void OnCopied(INode<T> oldNode, INode<T> newNode)
        {
            OnCopiedCore(oldNode, newNode, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnCopiedCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeNodeEventArgs<T>>)EventHandlerList?[CopiedEventKey];
            e?.Invoke(oldNode, new NodeTreeNodeEventArgs<T>(newNode));

            if (!IsRoot) GetNodeTree(Root).OnCopiedCore(oldNode, newNode, false);

            //if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
        }

        //-----------------------------------------------------------------------------
        // DeepCopy

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        protected virtual void OnDeepCopying(INode<T> oldNode, INode<T> newNode)
        {
            OnDeepCopyingCore(oldNode, newNode, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnDeepCopyingCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeNodeEventArgs<T>>)EventHandlerList?[DeepCopyingEventKey];
            e?.Invoke(oldNode, new NodeTreeNodeEventArgs<T>(newNode));

            if (!IsRoot) GetNodeTree(Root).OnDeepCopyingCore(oldNode, newNode, false);

            //if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        protected virtual void OnDeepCopied(INode<T> oldNode, INode<T> newNode)
        {
            OnDeepCopiedCore(oldNode, newNode, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        /// <param name="raiseValidate"></param>
        protected virtual void OnDeepCopiedCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
        {
            var e = (EventHandler<NodeTreeNodeEventArgs<T>>)EventHandlerList?[DeepCopiedEventKey];
            e?.Invoke(oldNode, new NodeTreeNodeEventArgs<T>(newNode));

            if (!IsRoot) GetNodeTree(Root).OnDeepCopiedCore(oldNode, newNode, false);

            //if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
        }

        //-----------------------------------------------------------------------------
    } // class NodeTree
}