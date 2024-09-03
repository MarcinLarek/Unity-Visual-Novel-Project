using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VariableStore;

public class VariableStoreTesting : MonoBehaviour
{
    public int var_int = 0;
    public float var_flt = 0f;
    public bool var_bool = false;
    public string var_str = "";


    // Start is called before the first frame update
    void Start()
    {
        VariableStore.CreateDatabase("DB_Links");

        VariableStore.CreateVariable("DB_Links.L_int", var_int, () => var_int, value => var_int = value);
        VariableStore.CreateVariable("DB_Links.L_flt", var_flt, () => var_flt, value => var_flt = value);
        VariableStore.CreateVariable("DB_Links.L_bool", var_bool, () => var_bool, value => var_bool = value);
        VariableStore.CreateVariable("DB_Links.L_str", var_str, () => var_str, value => var_str = value);




        VariableStore.CreateDatabase("DB_Numbers");
        VariableStore.CreateDatabase("DB_Booleans");

        VariableStore.CreateVariable("DB_Numbers.num1", 1);
        VariableStore.CreateVariable("DB_Numbers.num5", 5);
        VariableStore.CreateVariable("DB_Booleans.bool1", true);
        VariableStore.CreateVariable("DB_Numbers.float1", 7.5);
        VariableStore.CreateVariable("str1", "Hello");
        VariableStore.CreateVariable("str2", "World");

        VariableStore.PrintAllDatabases();
        VariableStore.PrintAllVariables();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            VariableStore.PrintAllVariables();

        if (Input.GetKeyUp(KeyCode.A))
        {
            string variable = "DB_Numbers.num1";
            VariableStore.TryGetValue(variable, out object v);
            VariableStore.TrySetValue(variable, (int)v +5);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            VariableStore.TryGetValue("DB_Numbers.num1", out object num1);
            VariableStore.TryGetValue("DB_Numbers.num5", out object num2);
            Debug.Log($"num1 + num5 = {(int)num1 + (int)num2}");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(VariableStore.TryGetValue("DB_Booleans.bool1", out object booleanv) && booleanv is bool)
                VariableStore.TrySetValue("DB_Booleans.bool1", !(bool)booleanv);

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            VariableStore.TryGetValue("str1", out object str_hello);
            VariableStore.TryGetValue("str2", out object str_world);

            VariableStore.TrySetValue("str1", (string)str_hello + str_world);

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            VariableStore.TryGetValue("DB_Links.L_int", out object linked_integer);
            VariableStore.TrySetValue("DB_Links.L_int", (int)linked_integer + 5);

            VariableStore.TryGetValue("DB_Links.L_flt", out object linked_float);
            VariableStore.TrySetValue("DB_Links.L_flt", (float)linked_float + 1.75f);

            VariableStore.TryGetValue("DB_Links.L_bool", out object linked_boolean);
            VariableStore.TrySetValue("DB_Links.L_bool", !(bool)linked_boolean);

            VariableStore.TryGetValue("DB_Links.L_str", out object linked_string);
            VariableStore.TrySetValue("DB_Links.L_str", (string)linked_string + Random.Range(1000,2000));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            VariableStore.RemoveVariable("DB_link.L_int");
            VariableStore.RemoveVariable("DB_booleans.bool1");

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            VariableStore.RemoveAllVariables();
        }

    }
}

