using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Dialect;
using System.Reflection;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Mapping.ByCode;
using NHibernateCodeCheat.mapping;
using NHibernate.Criterion;

namespace NHibernateCodeCheat.Tests {
    [TestClass]
    public class UnitTest1 {
        static ISessionFactory sessionFactory;

        [ClassInitialize()]
        public static void Initialize(TestContext context) {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x => {
                x.ConnectionString = "Server=localhost;Database=test;Uid=root;Pwd=kmn23po;";
                x.Driver<MySqlDataDriver>();
                x.Dialect<MySQLDialect>();
                x.LogSqlInConsole = true;
                x.BatchSize = 30;
            });

            var mapper = new ModelMapper();
            mapper.AddMapping<ParentMap>();
            mapper.AddMapping<ParentWithGuidMap>();
            mapper.AddMapping<ChildMap>();

            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            sessionFactory = cfg.BuildSessionFactory();            

            var schemaUpdate = new SchemaUpdate(cfg);
            schemaUpdate.Execute(false, true);

            InsertData();
        }

        public static void InsertData() {
            var parent = new Parent();
            parent.ParentName = "Teste";
            parent.ValueObject = new ValueObject();
            parent.ValueObject.value1 = "Value1";
            parent.ValueObject.value2 = "Value2";

            using (ISession session = sessionFactory.OpenSession()) {
                using (ITransaction transaction = session.BeginTransaction()) {
                    session.Save(parent);
                    transaction.Commit();
                }
            }
        }

        [TestMethod]
        public void BatchInsertTest() {
            using (ISession session = sessionFactory.OpenSession()) {
                using (ITransaction tx = session.BeginTransaction()) {
                    for (int i = 0; i < 300; i++) {
                        var parent = new ParentWithGuid();
                        parent.ParentName = "Teste " + i;
                        session.Save(parent);
                    }
                    tx.Commit();
                }
            }
        }

        [TestMethod]
        public void InsertTest() {
            using (ISession session = sessionFactory.OpenSession()) {
                using (ITransaction tx = session.BeginTransaction()) {
                    for (int i = 0; i < 300; i++) {
                        var parent = new Parent();
                        parent.ParentName = "Teste " + i;
                        session.Save(parent);
                    }
                    tx.Commit();
                }
            }
        }

        [TestMethod]
        public void InsertWithChildrenTest() {
            using (ISession session = sessionFactory.OpenSession()) {
                var parent = new Parent();
                using (ITransaction tx = session.BeginTransaction()) {                    
                    parent.ParentName = "Teste";
                    session.Save(parent);

                    var child1 = new Child();
                    child1.ChildName = "Child1";
                    var child2 = new Child();
                    child1.ChildName = "Child2";
                    parent.Childs = new List<Child>();
                    parent.Childs.Add(child1);
                    parent.Childs.Add(child2);
                    session.Save(parent);
                    tx.Commit();
                }

                var criteria = session.CreateCriteria<Parent>();
                criteria.Add(Expression.Eq("Id",parent.Id));

                parent = null;
                parent = criteria.UniqueResult<Parent>();
                Assert.IsNotNull(parent);
                Assert.AreEqual(2, parent.Childs.Count);
            }
        }

        [TestMethod]
        public void FetchWithChildrenTest() {
            using (ISession session = sessionFactory.OpenSession()) {
                //var criteria = session.CreateCriteria<Parent>();
                //criteria.Add(Expression.Eq("Id", 1070));


                var parent = session.Get<Parent>(1070); ;
                Assert.IsNotNull(parent);
                Assert.AreEqual(1, parent.Childs.Count);
            }
        }

        [TestMethod]
        public void SearchTest() {
            using (ISession session = sessionFactory.OpenSession()) {
                using (ITransaction transaction = session.BeginTransaction()) {
                    var criteria = session.CreateCriteria<Parent>();
                    var parents = criteria.List<Parent>();
                    transaction.Commit();

                    foreach (var parent in parents) {
                        Console.WriteLine(parent.ParentName);    
                    }
                    
                } 
            }
        }
    }// class
}
