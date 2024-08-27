using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableStore
{
    private const string DEFAULT_DATABASE_NAME = "Default";
    public class Database
    {
        public Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
    }

    public abstract class Variable
    {
        public abstract void Get();
        public abstract void Set(object value);
    }

    public class Variable<T> : Variable
    {
        private T value;

        private Func<T> getter;
        private Action<T> setter;

        public Variable(T defaultValue = default, Func<T> getter = null, Action<T> setter = null)
        {
            value = defaultValue;

            if (getter == null)
                this.getter = () => value;
            else
                this.getter = getter;

            if (setter == null)
                this.setter = newValue => value = newValue;
            else
                this.setter = setter;
        }

        public override void Get() => getter();

        public override void Set(object newValue) => setter((T)newValue);
    }

    private static Dictionary<string, Database> databases = new Dictionary<string, Database>() { { DEFAULT_DATABASE_NAME, new Database() } };
    private static Database defaultDatabase => databases[DEFAULT_DATABASE_NAME];
    public static bool CreateDatabase(string name)
    {
        if (!databases.ContainsKey(name))
        {
            databases[name] = new Database();
            return true;
        }

        return false;
    }
    public static Database GetDatabase(string name)
    {
        if (name == string.Empty)
            return defaultDatabase;

        if (!databases.ContainsKey(name))
            CreateDatabase(name);

        return databases[name];
    }

    public static void PrintAllDatabases()
    {
        foreach(KeyValuePair<string, Database> dbEntry in databases)
        {
            Debug.Log($"Database: '{dbEntry.Key}'");
        }
    }
}
