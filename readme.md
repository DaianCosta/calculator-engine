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
Console.WriteLine($"Culture: {CultureInfo.CurrentCulture.Name}");
Console.WriteLine($"UI Culture: {CultureInfo.CurrentUICulture.Name}");
Console.WriteLine($"Now: {DateTime.Now}");
Console.WriteLine($"UtcNow: {DateTime.UtcNow}");
Console.WriteLine($"TimeZone: {TimeZoneInfo.Local.Id}");
```
