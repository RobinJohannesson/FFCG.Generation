﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FFCG.G4.Movies.Core.Tests
{
    [TestFixture]
    public class MovieCollectionTests
    {
        private MovieCollection _collection;

        [SetUp]
        public void SetUp()
        {
            _collection = new MovieCollection(new InMemoryStorage());
        }

        [Test]
        public void AddMovie_should_add_the_movie_to_the_collection()
        {
            var movie = new Movie(1, "Test");
            _collection.AddMovie(movie);

            _collection.Movies.Should().Contain(movie);
        }

        [Test]
        public void Should_not_be_able_to_add_the_same_movie_twice()
        {
            var movie = new Movie(1, "Test");

            _collection.AddMovie(movie);
            _collection.AddMovie(movie);

            _collection.Movies.Should().HaveCount(1);
        }

        [Test]
        public void Should_be_able_to_fetch_all_movies()
        {
            var movie = new Movie(1, "Test");

            _collection.AddMovie(movie);

            var movieFromCollection = _collection.Movies.First();

            movieFromCollection.Should().Be(movie);
        }
    }

    public class InMemoryStorage : IStorage
    {
        private readonly Dictionary<Type, Dictionary<object, object>> _entities;

        public InMemoryStorage()
        {
            _entities = new Dictionary<Type, Dictionary<object, object>>();
        } 

        public T Load<T>(object id)
        {
            return (T)_entities[typeof(T)][id];
        }

        public void Store(object obj)
        {
            var type = obj.GetType();
            var json = JsonConvert.SerializeObject(obj);
            var id = JToken.Parse(json)["Id"];

            if(!_entities.ContainsKey(type))
                _entities.Add(type, new Dictionary<object, object>());

            if (_entities[type].ContainsKey(id))
            {
                _entities[type][id] = obj;
            }
            else
            {
                _entities[type].Add(id, obj);
            }
            
        }

        public IEnumerable<T> All<T>()
        {
            return _entities[typeof (T)].Values.Select(value => (T)value);
        }

        public void Delete(object obj)
        {
            //Todo: remove duplication
            var type = obj.GetType();
            var json = JsonConvert.SerializeObject(obj);
            var id = JToken.Parse(json)["Id"];

            if (_entities.ContainsKey(type))
            {
                if (_entities[type].ContainsKey(id))
                {
                    _entities[type].Remove(id);
                }
            }
            
        }
    }
}