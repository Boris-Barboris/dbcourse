using System;
using WebPortal.DataBase;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumTests
{

	/// <summary>
	/// Fake in-memory database context
	/// </summary>
	class FakeDbContaner : SkySharkDbContainer
	{
		internal class FakeDbSet<T> : DbSet<T>, IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable where T : class
		{
			internal List<T> items = new List<T>();
			PropertyInfo key_property = null;

			public FakeDbSet()
			{
				Type t = typeof(T);
				var mda = t.GetCustomAttribute<MetadataTypeAttribute>();
				var properties = mda.MetadataClassType.GetProperties();
				var md_key_property = properties.First(p => p.GetCustomAttribute<KeyAttribute>() != null);
				key_property = t.GetProperty(md_key_property.Name);
				Assert.IsNotNull(key_property);
			}

			public override T Find(params object[] keyValues)
			{
				if (keyValues.Length != 1)
					throw new ArgumentException("Only one key property is supported by EF6");
				return items.Find(v => key_property.GetValue(v).Equals(keyValues[0]));
			}

			public override T Remove(T entity)
			{
				if (items.Remove(entity))
					return entity;
				else
					return null;
			}

			public override T Add(T entity)
			{
				items.Add(entity);
				return entity;
			}

			public IQueryProvider Provider
			{
				get
				{
					return items.AsQueryable().Provider;
				}
			}

			public IEnumerator<T> GetEnumerator()
			{
				return items.GetEnumerator();
			}

			public Expression Expression
			{
				get
				{
					return items.AsQueryable().Expression;
				}
			}

			public Type ElementType
			{
				get
				{
					return items.AsQueryable().ElementType;
				}
			}
		}

		public override int SaveChanges()
		{
			return 0;
		}

		FakeDbSet<Passenger> passengerSet = new FakeDbSet<Passenger>();
		FakeDbSet<User> userSet = new FakeDbSet<User>();
		FakeDbSet<Reservation> reservationSet = new FakeDbSet<Reservation>();
		FakeDbSet<Cancellation> cancellationSet = new FakeDbSet<Cancellation>();
		FakeDbSet<FlightDetails> flightDetailsSet = new FakeDbSet<FlightDetails>();

		public void clear()
		{
			passengerSet.items.Clear();
			userSet.items.Clear();
			reservationSet.items.Clear();
			cancellationSet.items.Clear();
			flightDetailsSet.items.Clear();
		}

		public override DbSet<Passenger> PassengerSet
		{
			get
			{
				return passengerSet;
			}
			set { }
		}

		public override DbSet<User> UserSet
		{
			get
			{
				return userSet;
			}
			set { }
		}

		public override DbSet<Reservation> ReservationSet
		{
			get
			{
				return reservationSet;
			}
			set { }
		}

		public override DbSet<Cancellation> CancellationSet
		{
			get
			{
				return cancellationSet;
			}
			set { }
		}

		public override DbSet<FlightDetails> FlightDetailsSet
		{
			get
			{
				return flightDetailsSet;
			}
			set { }
		}
	}

}