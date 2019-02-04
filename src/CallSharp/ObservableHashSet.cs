using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CallSharp
{
  /// <summary>
  /// Represents an observable set of values.
  /// </summary>
  /// <typeparam name="T">The type of elements in the hash set.</typeparam>    
  public sealed class ObservableHashSet<T> : ISet<T>, INotifyCollectionChanged, INotifyPropertyChanged, IDisposable
  {
    private SimpleMonitor monitor = new SimpleMonitor();
    private HashSet<T> hashSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableHashSet&lt;T&gt;"/> class.
    /// </summary>
    public ObservableHashSet()
    {
      hashSet = new HashSet<T>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableHashSet&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new set.</param>
    public ObservableHashSet(IEnumerable<T> collection)
    {
      hashSet = new HashSet<T>(collection);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableHashSet&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="comparer">The IEqualityComparer&lt;T&gt; implementation to use when comparing values in the set, or null to use the default EqualityComparer&lt;T&gt; implementation for the set type.</param>
    public ObservableHashSet(IEqualityComparer<T> comparer)
    {
      hashSet = new HashSet<T>(comparer);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableHashSet&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new set.</param>
    /// <param name="comparer">The IEqualityComparer&lt;T&gt; implementation to use when comparing values in the set, or null to use the default EqualityComparer&lt;T&gt; implementation for the set type.</param>
    public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
    {
      hashSet = new HashSet<T>(collection, comparer);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (monitor != null)
      {
        monitor.Dispose();
        monitor = null;
      }
    }

    #region Properties        

    /// <summary>
    /// The property names used with INotifyPropertyChanged.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "A container for constants used with INotifyPropertyChanged.")]
    public static class PropertyNames
    {
      public const string Count = "Count";
      public const string IsReadOnly = "IsReadOnly";
    }


    /// <summary>
    /// Gets the IEqualityComparer&lt;T&gt; object that is used to determine equality for the values in the set.
    /// </summary>
    public IEqualityComparer<T> Comparer => hashSet.Comparer;

    /// <summary>
    /// Gets the number of elements contained in the <see cref="ObservableHashSet&lt;T&gt;"/>.
    /// </summary>
    /// <returns>
    /// The number of elements contained in the <see cref="ObservableHashSet&lt;T&gt;"/>.
    ///   </returns>
    public int Count => hashSet.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
    ///   </returns>
    bool ICollection<T>.IsReadOnly => ((ICollection<T>)hashSet).IsReadOnly;

  #endregion

    #region Events

    /// <summary>
    /// Raised when the collection changes.
    /// </summary>
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (CollectionChanged != null)
      {
        using (BlockReentrancy())
        {
          CollectionChanged(this, e);
        }
      }
    }

    /// <summary>
    /// Raised when a property value changes.
    /// </summary>       
    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds the specified element to a set.
    /// </summary>
    /// <param name="item">The element to add to the set.</param>
    /// <returns>true if the element is added to the <see cref="ObservableHashSet&lt;T&gt;"/> object; false if the element is already present.</returns>
    public bool Add(T item)
    {
      CheckReentrancy();

      bool wasAdded = hashSet.Add(item);

      if (wasAdded)
      {
        int index = hashSet.IndexOf(item);
        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        RaisePropertyChanged(PropertyNames.Count);
      }

      return wasAdded;
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
    ///   </exception>
    void ICollection<T>.Add(T item)
    {
      Add(item);
    }

    /// <summary>
    /// Removes all elements from a <see cref="ObservableHashSet&lt;T&gt;"/> object.
    /// </summary>        
    public void Clear()
    {
      CheckReentrancy();

      if (hashSet.Count > 0)
      {
        hashSet.Clear();

        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        RaisePropertyChanged(PropertyNames.Count);
      }
    }

    /// <summary>
    /// Determines whether a <see cref="ObservableHashSet&lt;T&gt;"/> object contains the specified element.
    /// </summary>
    /// <param name="item">The element to locate in the <see cref="ObservableHashSet&lt;T&gt;"/> object.</param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object contains the specified element; otherwise, false.</returns>
    public bool Contains(T item)
    {
      return hashSet.Contains(item);
    }


    /// <summary>
    /// Copies the elements of a <see cref="ObservableHashSet&lt;T&gt;"/> collection to an array.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="ObservableHashSet&lt;T&gt;"/> object. The array must have zero-based indexing.</param>
    public void CopyTo(T[] array)
    {
      hashSet.CopyTo(array);
    }

    /// <summary>
    /// Copies the elements of a <see cref="ObservableHashSet&lt;T&gt;"/> collection to an array.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="ObservableHashSet&lt;T&gt;"/> object. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex)
    {
      hashSet.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Copies the elements of a <see cref="ObservableHashSet&lt;T&gt;"/> collection to an array.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="ObservableHashSet&lt;T&gt;"/> object. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    /// <param name="count">The number of elements to copy to array.</param>
    public void CopyTo(T[] array, int arrayIndex, int count)
    {
      hashSet.CopyTo(array, arrayIndex, count);
    }

    /// <summary>
    /// Removes all elements in the specified collection from the current <see cref="ObservableHashSet&lt;T&gt;"/> object.
    /// </summary>
    /// <param name="other">The collection of items to remove from the <see cref="ObservableHashSet&lt;T&gt;"/> object.</param>        
    public void ExceptWith(IEnumerable<T> other)
    {
      //VerifyArgument.IsNotNull("other", other);

      CheckReentrancy();

      // I locate items in other that are in the hashset
      var removedItems = other.Where(x => hashSet.Contains(x)).ToList();

      hashSet.ExceptWith(other);

      if (removedItems.Count > 0)
      {
        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems));
        RaisePropertyChanged(PropertyNames.Count);
      }
    }

    /// <summary>
    /// Returns an enumerator that iterates through a <see cref="ObservableHashSet&lt;T&gt;"/>.
    /// </summary>
    /// <returns>A <see cref="ObservableHashSet&lt;T&gt;"/>.Enumerator object for the <see cref="ObservableHashSet&lt;T&gt;"/> object.</returns>
    public IEnumerator<T> GetEnumerator()
    {
      return hashSet.GetEnumerator();
    }

    /// <summary>
    /// Modifies the current <see cref="ObservableHashSet&lt;T&gt;"/> object to contain only elements that are present in that object and in the specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object.</param>
    public void IntersectWith(IEnumerable<T> other)
    {
      //VerifyArgument.IsNotNull("other", other);

      CheckReentrancy();

      // I locate the items in the hashset that are not in other
      var removedItems = hashSet.Where(x => !other.Contains(x)).ToList();

      hashSet.IntersectWith(other);

      if (removedItems.Count > 0)
      {
        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems));
        RaisePropertyChanged(PropertyNames.Count);
      }
    }

    /// <summary>
    /// Determines whether a <see cref="ObservableHashSet&lt;T&gt;"/> object is a proper subset of the specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object.</param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object is a proper subset of other; otherwise, false.</returns>
    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      return hashSet.IsProperSubsetOf(other);
    }

    /// <summary>
    /// Determines whether a <see cref="ObservableHashSet&lt;T&gt;"/> object is a proper subset of the specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object. </param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object is a proper superset of other; otherwise, false.</returns>
    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      return hashSet.IsProperSupersetOf(other);
    }

    /// <summary>
    /// Determines whether a <see cref="ObservableHashSet&lt;T&gt;"/> object is a subset of the specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object. </param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object is a subset of other; otherwise, false.</returns>
    public bool IsSubsetOf(IEnumerable<T> other)
    {
      return hashSet.IsSubsetOf(other);
    }

    /// <summary>
    /// Determines whether a <see cref="ObservableHashSet&lt;T&gt;"/> object is a superset of the specified collection.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object. </param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object is a superset of other; otherwise, false.</returns>
    public bool IsSupersetOf(IEnumerable<T> other)
    {
      return hashSet.IsSupersetOf(other);
    }

    /// <summary>
    /// Determines whether the current <see cref="ObservableHashSet&lt;T&gt;"/> object and a specified collection share common elements.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object. </param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object and other share at least one common element; otherwise, false.</returns>
    public bool Overlaps(IEnumerable<T> other)
    {
      return hashSet.Overlaps(other);
    }

    /// <summary>
    /// Removes the specified element from a <see cref="ObservableHashSet&lt;T&gt;"/> object.
    /// </summary>
    /// <param name="item">The element to remove.</param>
    /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if item is not found in the <see cref="ObservableHashSet&lt;T&gt;"/> object.</returns>
    public bool Remove(T item)
    {
      int index = hashSet.IndexOf(item);
      bool wasRemoved = hashSet.Remove(item);

      if (wasRemoved)
      {

        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        RaisePropertyChanged(PropertyNames.Count);
      }

      return wasRemoved;
    }

    /// <summary>
    /// Determines whether a <see cref="ObservableHashSet&lt;T&gt;"/> object and the specified collection contain the same elements.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object. </param>
    /// <returns>true if the <see cref="ObservableHashSet&lt;T&gt;"/> object is equal to other; otherwise, false.</returns>
    public bool SetEquals(IEnumerable<T> other)
    {
      return hashSet.SetEquals(other);
    }

    /// <summary>
    /// Modifies the current <see cref="ObservableHashSet&lt;T&gt;"/> object to contain only elements that are present either in that object or in the specified collection, but not both.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object.</param>
    public void SymmetricExceptWith(IEnumerable<T> other)
    {
      //VerifyArgument.IsNotNull("other", other);
      CheckReentrancy();

      // I locate the items in other that are not in the hashset
      var addedItems = other.Where(x => !hashSet.Contains(x)).ToList();

      // I locate items in other that are in the hashset
      var removedItems = other.Where(x => hashSet.Contains(x)).ToList();

      hashSet.SymmetricExceptWith(other);

      if (removedItems.Count > 0)
      {
        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems));
        RaisePropertyChanged(PropertyNames.Count);
      }

      if (addedItems.Count > 0)
      {
        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, addedItems));
      }

      if (removedItems.Count > 0 || addedItems.Count > 0)
      {
        RaisePropertyChanged(PropertyNames.Count);
      }
    }

    /// <summary>
    /// Sets the capacity of a <see cref="ObservableHashSet&lt;T&gt;"/> object to the actual number of elements it contains, rounded up to a nearby, implementation-specific value.
    /// </summary>
    public void TrimExcess()
    {
      hashSet.TrimExcess();
    }

    /// <summary>
    /// Modifies the current <see cref="ObservableHashSet&lt;T&gt;"/> object to contain all elements that are present in itself, the specified collection, or both.
    /// </summary>
    /// <param name="other">The collection to compare to the current <see cref="ObservableHashSet&lt;T&gt;"/> object.</param>
    public void UnionWith(IEnumerable<T> other)
    {
      //VerifyArgument.IsNotNull("other", other);
      CheckReentrancy();

      // I locate the items in other that are not in the hashset
      var addedItems = other.Where(x => !hashSet.Contains(x)).ToList();

      hashSet.UnionWith(other);

      if (addedItems.Count > 0)
      {
        RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, addedItems));
        RaisePropertyChanged(PropertyNames.Count);
      }
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)hashSet).GetEnumerator();
    }

    #endregion

    #region Reentrancy Methods

    private IDisposable BlockReentrancy()
    {
      monitor.Enter();
      return monitor;
    }

    private void CheckReentrancy()
    {
      if ((monitor.Busy && (CollectionChanged != null)) && (CollectionChanged.GetInvocationList().Length > 1))
      {
        throw new InvalidOperationException("There are additional attempts to change this hash set during a CollectionChanged event.");
      }
    }

    #endregion

    #region Private Classes

    private class SimpleMonitor : IDisposable
    {
      private int _busyCount;

      public void Dispose()
      {
        _busyCount--;
      }

      public void Enter()
      {
        _busyCount++;
      }

      public bool Busy => (_busyCount > 0);
    }


    #endregion
  }
}