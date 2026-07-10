# 🧮 Calculation Engine - Exemplos de JSON

Este documento contém exemplos de requisições para testar o motor de cálculo via API.

---

## 🧪 1. Correção Monetária (IPCA mock)

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "indices": [
            { "mes": "2024-01", "valor": 0.005 },
            { "mes": "2024-02", "valor": 0.004 }
          ]
        }
      }
    ]
  }
}
```

---

## 🧪 2. Juros Simples

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01
        }
      }
    ]
  }
}
```

---

## 🧪 3. Juros Compostos

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "composto",
          "taxa": 0.01
        }
      }
    ]
  }
}
```

---

## 🧪 4. Sem Juros

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "nenhum"
        }
      }
    ]
  }
}
```

---

## 🧪 5. Juros com Carência

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01,
          "carenciaDias": 30
        }
      }
    ]
  }
}
```

---

## 🧪 6. Juros sobre Valor Original

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01,
          "incidencia": "valor_original"
        }
      }
    ]
  }
}
```

---

## 🧪 7. Correção + Juros Simples

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "indices": [
            { "mes": "2024-01", "valor": 0.005 },
            { "mes": "2024-02", "valor": 0.004 }
          ]
        }
      },
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01
        }
      }
    ]
  }
}
```

---

## 🧪 8. Pipeline Completa (Realista)

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "indices": [
            { "mes": "2024-01", "valor": 0.005 },
            { "mes": "2024-02", "valor": 0.004 }
          ]
        }
      },
      {
        "type": "juros",
        "params": {
          "tipo": "composto",
          "taxa": 0.01
        }
      },
      {
        "type": "multa",
        "params": {
          "percentual": 0.02
        }
      },
      {
        "type": "encargos",
        "params": {
          "valor": 50
        }
      }
    ]
  }
}
```

---

## 🧪 9. Teste de Ordem (Importante)

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01
        }
      },
      {
        "type": "correcao_monetaria",
        "params": {
          "indices": [
            { "mes": "2024-01", "valor": 0.005 },
            { "mes": "2024-02", "valor": 0.004 }
          ]
        }
      }
    ]
  }
}
```

---

# 💡 Observações

* A ordem dos steps altera o resultado final
* Correção monetária usa fator acumulado (não soma)
* Juros pode ser simples, composto ou inexistente
* O motor é totalmente configurável via JSON

---

# 🚀 Próximos passos

* Criar testes automatizados (xUnit)
* Integrar índices reais (IPCA, IGPM)
* Persistir configurações no banco (MongoDB)
* Gerar PDF da memória de cálculo

```
import os
import csv
from dotenv import load_dotenv
from pymongo import MongoClient
import pyodbc

import sys
sys.stdout.reconfigure(encoding='utf-8')

load_dotenv()

# 🔌 ENV
MONGO_URI = os.getenv("PRD_MONGO_URI")
SQL_CONN = os.getenv("SQL_CONNECTION_I4PRO_PRD")

# 🔌 MONGO
mongo_client = MongoClient(MONGO_URI)
mongo_db = mongo_client["ClaimEngine"]  # ajuste
users_col = mongo_db["User"]

# 🔌 SQL
if not SQL_CONN:
    raise Exception("❌ SQL_CONNECTION_I4PRO_PRD não encontrada")

sql_conn = pyodbc.connect(SQL_CONN)
cursor = sql_conn.cursor()

# 🔥 CACHE
produtos_cache = {}
ramos_cache = {}
coberturas_cache = {}

# ---------------------------
# 🔎 FUNÇÕES SQL
# ---------------------------

def get_produto(cd_produto):
    if cd_produto in produtos_cache:
        return produtos_cache[cd_produto]

    cursor.execute(
        "SELECT nm_produto FROM corp_produto WHERE cd_produto = ?",
        cd_produto
    )
    row = cursor.fetchone()
    nome = row[0] if row else None

    produtos_cache[cd_produto] = nome
    return nome


def get_ramo(nr_ramo):
    if nr_ramo in ramos_cache:
        return ramos_cache[nr_ramo]

    cursor.execute(
        "SELECT rm_ramo FROM corp_ramo WHERE nr_ramo = ?",
        nr_ramo
    )
    row = cursor.fetchone()
    nome = row[0] if row else None

    ramos_cache[nr_ramo] = nome
    return nome


def get_cobertura(cd_produto, id_cobertura):
    key = (cd_produto, id_cobertura)

    if key in coberturas_cache:
        return coberturas_cache[key]

    query = """
        SELECT cpc.nm_comercial
        FROM corp_produto_cobertura cpc
        INNER JOIN corp_cobertura cp 
            ON cp.id_cobertura = cpc.id_cobertura
        WHERE cpc.cd_produto = ?
          AND cpc.id_cobertura = ?
    """

    cursor.execute(query, cd_produto, id_cobertura)
    row = cursor.fetchone()

    nome = row[0] if row else None
    coberturas_cache[key] = nome
    return nome


# ---------------------------
# 🚀 PROCESSAMENTO
# ---------------------------

resultado = []

for user in users_col.find():
    skills = user.get("skills", {})

    produtos_ids = skills.get("products", [])
    branch_ids = skills.get("branchs", [])
    coverage_ids = skills.get("coverageCodes", [])

    # 🔹 PRODUTOS
    produtos_nomes = []
    for p in produtos_ids:
        try:
            cd = int(p)
            nome = get_produto(cd)
            if nome:
                produtos_nomes.append(nome)
        except:
            continue

    # 🔹 RAMOS
    ramos_nomes = []
    for r in branch_ids:
        try:
            nr = int(r)
            nome = get_ramo(nr)
            if nome:
                ramos_nomes.append(nome)
        except:
            continue

    # 🔹 COBERTURAS
    coberturas_nomes = []

    # ⚠️ relação precisa do produto
    produto_base = int(produtos_ids[0]) if produtos_ids else None

    for c in coverage_ids:
        try:
            id_cov = int(c)
            if produto_base:
                nome = get_cobertura(produto_base, id_cov)
                if nome:
                    coberturas_nomes.append(nome)
        except:
            continue

    # 📊 LINHA FINAL (TODOS CAMPOS)
    resultado.append({
        "_id": str(user.get("_id")),
        "userCode": user.get("userCode"),
        "userName": user.get("userName"),
        "userId": user.get("userId"),
        "role": user.get("role"),
        "isActive": user.get("isActive"),
        "createdAt": user.get("createdAt"),
        "updatedAt": user.get("updatedAt"),
        "updatedUserName": user.get("updatedUserName"),

        "products_ids": ", ".join(produtos_ids),
        "products_names": ", ".join(produtos_nomes),

        "branch_ids": ", ".join(branch_ids),
        "branch_names": ", ".join(ramos_nomes),

        "coverage_ids": ", ".join(map(str, coverage_ids)),
        "coverage_names": ", ".join(coberturas_nomes),

        "steps": ", ".join(skills.get("steps", [])),
        "actions": ", ".join(skills.get("actions", [])),
    })

    print(f"✔ Processado: {user.get('userCode')}")

# ---------------------------
# 📄 CSV
# ---------------------------

with open("users_full_export.csv", "w", newline="", encoding="utf-8") as f:
    writer = csv.DictWriter(f, fieldnames=resultado[0].keys())
    writer.writeheader()
    writer.writerows(resultado)

print("✅ CSV gerado: users_full_export.csv")

```
