using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

public class CommandManager : MonoBehaviour
{
    public static CommandManager instance { get; private set; }

    private CommandDatabase database;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            database = new CommandDatabase();

            //Finds all extension and exectue the Extend method on them
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] extensionTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(CMD_DatabaseExtension))).ToArray();

            foreach(Type extension in extensionTypes)
            {
                MethodInfo extendMethod = extension.GetMethod("Extend");
                extendMethod.Invoke(null, new object[] { database });
            }
        }
        else
            DestroyImmediate(gameObject);
    }

    public void Execute(string commandName)
    {
        Delegate command = database.GetCommand(commandName);

        if (command != null)
            command.DynamicInvoke();
    }

}
