# 🧮 Calculation Engine - Testes de API

Este documento contém exemplos de requisições JSON para testar o motor de cálculo financeiro.

---

# 📌 Estrutura da requisição

```json
{
  "input": {
    "valorOriginal": 1000,
    "diasAtraso": 60
  },
  "config": {
    "steps": []
  }
}
```

---

# 🧪 1. Correção Monetária (Pró-rata diário)

```json
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 120
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "periodos": [
            { "mes": "2025-10", "indice": 0.0009, "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "indice": 0.0018, "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "indice": 0.0070, "dias": 10, "diasMes": 28 }
          ]
        }
      }
    ]
  }
}
```

---

# 🧪 2. Juros Simples (pró-rata)

```json
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 120
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01,
          "periodos": [
            { "mes": "2025-10", "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "dias": 10, "diasMes": 28 }
          ]
        }
      }
    ]
  }
}
```

---

# 🧪 3. Juros Compostos (pró-rata)

```json
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 120
  },
  "config": {
    "steps": [
      {
        "type": "juros",
        "params": {
          "tipo": "composto",
          "taxa": 0.01,
          "periodos": [
            { "mes": "2025-10", "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "dias": 10, "diasMes": 28 }
          ]
        }
      }
    ]
  }
}
```

---

# 🧪 4. Sem Juros

```json
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 120
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

# 🧪 5. Correção + Juros Simples

```json
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 120
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "periodos": [
            { "mes": "2025-10", "indice": 0.0009, "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "indice": 0.0018, "dias": 30, "diasMes": 30 }
          ]
        }
      },
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01,
          "periodos": [
            { "mes": "2025-10", "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "dias": 30, "diasMes": 30 }
          ]
        }
      }
    ]
  }
}
```

---

# 🧪 6. Pipeline Completa (Realista)

```json
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 120
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "periodos": [
            { "mes": "2025-10", "indice": 0.0009, "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "indice": 0.0018, "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "indice": 0.0070, "dias": 10, "diasMes": 28 }
          ]
        }
      },
      {
        "type": "juros",
        "params": {
          "tipo": "composto",
          "taxa": 0.01,
          "periodos": [
            { "mes": "2025-10", "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "dias": 10, "diasMes": 28 }
          ]
        }
      },
      {
        "type": "multa",
        "params": {
          "percentual": 0.02
        }
      }
    ]
  }
}
```

# 🧪 7. Pipeline Completa Simples (Too)
```
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 118
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "periodos": [
            { "mes": "2025-10", "indice": 0.0009, "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "indice": 0.0018, "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "indice": 0.0070, "dias": 10, "diasMes": 28 }
          ]
        }
      },
      {
        "type": "juros",
        "params": {
          "tipo": "simples",
          "taxa": 0.01,
          "periodos": [
            { "mes": "2025-10", "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "dias": 10, "diasMes": 28 }
          ]
        }
      },
      {
        "type": "multa",
        "params": {
          "percentual": 0.02
        }
      }
    ]
  }
}
```

# 🧪 8. Pipeline Completa Composto (Too)
```
{
  "input": {
    "valorOriginal": 10000,
    "diasAtraso": 118
  },
  "config": {
    "steps": [
      {
        "type": "correcao_monetaria",
        "params": {
          "periodos": [
            { "mes": "2025-10", "indice": 0.0009, "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "indice": 0.0018, "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "indice": 0.0033, "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "indice": 0.0070, "dias": 10, "diasMes": 28 }
          ]
        }
      },
      {
        "type": "juros",
        "params": {
          "tipo": "composto",
          "taxa": 0.01,
          "periodos": [
            { "mes": "2025-10", "dias": 16, "diasMes": 31 },
            { "mes": "2025-11", "dias": 30, "diasMes": 30 },
            { "mes": "2025-12", "dias": 31, "diasMes": 31 },
            { "mes": "2026-01", "dias": 31, "diasMes": 31 },
            { "mes": "2026-02", "dias": 10, "diasMes": 28 }
          ]
        }
      },
      {
        "type": "multa",
        "params": {
          "percentual": 0.02
        }
      }
    ]
  }
}
```

---

# ⚠️ Observações Importantes

* A ordem dos steps altera o resultado final
* Correção monetária usa fator acumulado (multiplicação)
* Juros simples soma fatores
* Juros composto multiplica fatores
* Meses incompletos usam pró-rata diário

---

# 🚀 Próximos passos

* Implementar testes automatizados (xUnit)
* Integrar índices reais (IPCA / IGPM)
* Persistir configurações no MongoDB
* Gerar PDF da memória de cálculo

```
```

quero fazer um procv onde quero comparar e pegar os valores abaixo:

coluna "Base!H2" é igual a coluna "coberturas!B2"
coluna "Base!B2" é igual a coluna "coberturas!F2"
obter o valor da coluna "coberturas!E2" e atribuir na coluna "Base!AB2"
