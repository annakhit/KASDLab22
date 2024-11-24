using System;
using System.Collections.Generic;

public class MyTreeMap<K, V> where K : IComparable<K> where V : IComparable<V>
{
    protected int Count { get; private set; }

    protected Node<K, V> Root;

    protected IComparer<K> Comparator = Comparer<K>.Create((a, b) => a.CompareTo(b));

    public MyTreeMap() { }
    public MyTreeMap(IComparer<K> Comparator)
    {
        this.Comparator = Comparator;
    }

    public void Clear()
    {
        Root = null;
        Count = 0;
    }

    public bool ContainsKey(K key)
    {
        Node<K, V> node = Root;

        while (node != null)
        {
            int compare = Comparator.Compare(key, node.Key);

            if (compare == 0) return true;

            node = compare < 0 ? node.Left : node.Right;
        }

        return false;
    }

    public bool ContainsValue(V value) => ContainsValueRecursive(Root, value);

    private bool ContainsValueRecursive(Node<K, V> node, V value)
    {
        if (node == null) return false;

        if (node.Value.CompareTo(value) == 0) return true;

        bool left = ContainsValueRecursive(node.Left, value);
        bool right = ContainsValueRecursive(node.Right, value);

        return left ? left : right;
    }

    public IEnumerable<KeyValuePair<K, V>> EntrySet()
    {
        List<KeyValuePair<K, V>> nodes = new List<KeyValuePair<K, V>>();

        AddNodesRecursive(Root, nodes);

        foreach (KeyValuePair<K, V> node in nodes) yield return node;
    }

    private void AddNodesRecursive(Node<K, V> node, ICollection<KeyValuePair<K, V>> collection)
    {
        if (node == null) return;

        AddNodesRecursive(node.Left, collection);

        collection.Add(new KeyValuePair<K, V>(node.Key, node.Value));

        AddNodesRecursive(node.Right, collection);
    }

    public V Get(K key)
    {

        Node<K, V> node = Root;

        while (node != null)
        {
            int compare = Comparator.Compare(key, node.Key);

            if (compare == 0) return node.Value;

            node = compare < 0 ? node.Left : node.Right;
        }

        return default;
    }

    public bool IsEmpty() => Count == 0;

    public HashSet<K> KeySet()
    {
        var keys = new HashSet<K>();
        AddKeysRecursive(Root, keys);
        return keys;
    }

    private void AddKeysRecursive(Node<K, V> node, ICollection<K> collection)
    {
        if (node == null) return;

        AddKeysRecursive(node.Left, collection);

        collection.Add(node.Key);

        AddKeysRecursive(node.Right, collection);
    }

    public V Put(K key, V value)
    {
        Root = PutRecursive(key, value, Root);

        return value;
    }
    private Node<K, V> PutRecursive(K key, V value, Node<K, V> node)
    {
        if (node == null)
        {
            Count++;
            return new Node<K, V>(key, value);
        }

        int comp = Comparator.Compare(key, node.Key);

        if (comp < 0)
        {
            node.Left = PutRecursive(key, value, node.Left);

            if (Height(node.Left) - Height(node.Right) == 2)
            {
                int comp2 = Comparator.Compare(key, node.Left.Key);
                node = (comp2 < 0) ? RightRotation(node) : DoubleRightRotation(node);
            }
        }
        else if (comp > 0)
        {
            node.Right = PutRecursive(key, value, node.Right);

            if (Height(node.Right) - Height(node.Left) == 2)
            {
                int comp2 = Comparator.Compare(node.Right.Key, key);
                node = (comp2 < 0) ? LeftRotation(node) : DoubleLeftRotation(node);
            }
        }
        else
        {
            node.Value = value;
            return node;
        }

        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

        return node;
    }

    private int Height(Node<K, V> node) => node == null ? -1 : node.Height;

    private Node<K, V> RightRotation(Node<K, V> n2)
    {
        Node<K, V> n1 = n2.Left;

        n2.Left = n1.Right;
        n1.Right = n2;

        n2.Height = Math.Max(Height(n2.Left), Height(n2.Right)) + 1;
        n1.Height = Math.Max(Height(n1.Left), Height(n2)) + 1;

        return n1;
    }

    private Node<K, V> LeftRotation(Node<K, V> n1)
    {
        Node<K, V> n2 = n1.Right;

        n1.Right = n2.Left;
        n2.Left = n1;

        n1.Height = Math.Max(Height(n1.Left), Height(n1.Right)) + 1;
        n2.Height = Math.Max(Height(n2.Right), Height(n1)) + 1;

        return n2;
    }

    private Node<K, V> DoubleRightRotation(Node<K, V> n3)
    {
        n3.Left = LeftRotation(n3.Left);

        return RightRotation(n3);
    }

    private Node<K, V> DoubleLeftRotation(Node<K, V> n1)
    {
        n1.Right = RightRotation(n1.Right);

        return LeftRotation(n1);
    }

    public V Remove(K key)
    {
        Node<K, V> removed = new Node<K, V>();

        Root = RemoveRecursive(key, removed, Root);

        return removed.Value;
    }

    private Node<K, V> RemoveRecursive(K key, Node<K, V> removed, Node<K, V> node)
    {
        if (node == null) return null;

        int compare = Comparator.Compare(key, node.Key);

        if (compare < 0) node.Left = RemoveRecursive(key, removed, node.Left);

        else if (compare > 0) node.Right = RemoveRecursive(key, removed, node.Right);

        else if (node.Left != null && node.Right != null)
        {
            Node<K, V> maxNode = GetMax(node.Left);

            removed.Key = node.Key;
            removed.Value = node.Value;

            node.Key = maxNode.Key;
            node.Value = maxNode.Value;
            node.Left = RemoveMax(node.Left);

            Count--;
        }
        else
        {
            removed.Key = node.Key;
            removed.Value = node.Value;
            node = node.Left ?? node.Right;
            Count--;
        }

        return RebalanceTree(node);
    }

    private Node<K, V> RemoveMax(Node<K, V> node)
    {
        if (node == null) return null;
        else if (node.Right != null)
        {
            node.Right = RemoveMax(node.Right);
            return node;
        }
        return node.Left;
    }

    private Node<K, V> GetMax(Node<K, V> node)
    {
        Node<K, V> parent = null;

        while (node != null)
        {
            parent = node;
            node = node.Right;
        }

        return parent;
    }

    private Node<K, V> RebalanceTree(Node<K, V> node)
    {
        if (node == null) return null;

        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

        int balance = Height(node.Left) - Height(node.Right);

        if (balance > 1 && Height(node.Left.Left) - Height(node.Left.Right) >= 0)
        {
            return RightRotation(node);
        }

        if (balance > 1 && Height(node.Left.Left) - Height(node.Left.Right) < 0)
        {
            node.Left = LeftRotation(node.Left);
            return RightRotation(node);
        }

        if (balance < -1 && Height(node.Right.Left) - Height(node.Right.Right) <= 0)
        {
            return LeftRotation(node);
        }

        if (balance < -1 && Height(node.Right.Left) - Height(node.Right.Right) > 0)
        {
            node.Right = RightRotation(node.Right);
            return LeftRotation(node);
        }

        return node;
    }

    public int Size() => Count;

    public K FirstKey()
    {
        Node<K, V> node = Root;

        while (node != null)
        {
            if (node.Left == null) return node.Key;
            node = node.Left;
        }

        return default;
    }

    public K LastKey()
    {
        Node<K, V> node = Root;

        while (node != null)
        {
            if (node.Right == null) return node.Key;
            node = node.Right;
        }

        return default;
    }

    public MyTreeMap<K, V> HeadMap(K to, bool equal = false)
    {
        MyTreeMap<K, V> map = new MyTreeMap<K, V>();

        HeadMapRecursive(to, Root, map, equal);

        return map;
    }

    private void HeadMapRecursive(K key, Node<K, V> node, MyTreeMap<K, V> map, bool equal)
    {
        if (node == null) return;

        HeadMapRecursive(key, node.Left, map, equal);

        int compare = Comparator.Compare(key, node.Key);

        if (compare > 0) map.Put(node.Key, node.Value);

        if (compare == 0 && equal) map.Put(node.Key, node.Value);

        HeadMapRecursive(key, node.Right, map, equal);
    }

    public MyTreeMap<K, V> SubMap(K fromKey, K toKey)
    {

        MyTreeMap<K, V> map = new MyTreeMap<K, V>();

        SubMapRecursive(fromKey, toKey, Root, map);

        return map;
    }

    private void SubMapRecursive(K from, K to, Node<K, V> node, MyTreeMap<K, V> map)
    {
        if (node == null) return;

        SubMapRecursive(from, to, node.Left, map);

        int compare1 = Comparator.Compare(from, node.Key);
        int compare2 = Comparator.Compare(to, node.Key);

        if (compare1 <= 0 && compare2 > 0) map.Put(node.Key, node.Value);

        SubMapRecursive(from, to, node.Right, map);
    }

    public MyTreeMap<K, V> TailMap(K from, bool equal = false)
    {

        MyTreeMap<K, V> map = new MyTreeMap<K, V>();

        TailMapRecursive(from, Root, map, equal);

        return map;
    }

    private void TailMapRecursive(K key, Node<K, V> node, MyTreeMap<K, V> map, bool equal)
    {
        if (node == null) return;

        TailMapRecursive(key, node.Left, map, equal);

        int compare = Comparator.Compare(key, node.Key);

        if (compare < 0) map.Put(node.Key, node.Value);

        if (compare == 0 && equal) map.Put(node.Key, node.Value);

        TailMapRecursive(key, node.Right, map, equal);
    }

    public KeyValuePair<K, V> LowerEntry(K key)
    {
        MyTreeMap<K, V> map = HeadMap(key);

        Node<K, V> node = map.Root;

        while (node != null)
        {
            if (node.Right == null) return new KeyValuePair<K, V>(node.Key, node.Value);
            node = node.Right;
        }

        return default;
    }

    public KeyValuePair<K, V> FloorEntry(K key)
    {
        MyTreeMap<K, V> map = HeadMap(key, true);

        Node<K, V> node = map.Root;

        while (node != null)
        {
            if (node.Right == null) return new KeyValuePair<K, V>(node.Key, node.Value);
            node = node.Right;
        }

        return default;
    }

    public KeyValuePair<K, V> HigherEntry(K key)
    {
        MyTreeMap<K, V> map = TailMap(key);

        Node<K, V> node = map.Root;

        while (node != null)
        {
            if (node.Left == null) return new KeyValuePair<K, V>(node.Key, node.Value);
            node = node.Left;
        }

        return default;
    }

    public KeyValuePair<K, V> CeilingEntry(K key)
    {
        MyTreeMap<K, V> map = TailMap(key, true);

        Node<K, V> node = map.Root;

        while (node != null)
        {
            if (node.Left == null) return new KeyValuePair<K, V>(node.Key, node.Value);
            node = node.Left;
        }

        return default;
    }

    public K LowerKey(K key) => LowerEntry(key).Key;

    public K FloorKey(K key) => FloorEntry(key).Key;

    public K HigherKey(K key) => HigherEntry(key).Key;

    public K PollFirstEntry(K key) => CeilingEntry(key).Key;

    public V PollFirstEntry() => Remove(FirstKey());

    public V PollLastEntry() => Remove(LastKey());

    public V FirstEntry() => Get(FirstKey());
    public V LastEntry() => Get(LastKey());
}

