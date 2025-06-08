# 🎮 Jogo de Plataforma 2D com Unity 6

Este é um projeto educacional de jogo de plataforma em 2D desenvolvido no Unity 6, com foco em boas práticas de arquitetura, modularização de código e animações. Ideal para estudantes, professores e iniciantes em desenvolvimento de jogos.

---

## 📁 Estrutura do Projeto

O código é organizado em scripts modulares, seguindo o princípio da responsabilidade única (SRP):

| Script                | Função Principal                                           |
|-----------------------|------------------------------------------------------------|
| `JogadorInput`        | Captura entradas brutas (movimento e pulo)                |
| `JogadorMovimento`    | Aplica movimento horizontal usando física (Rigidbody2D)    |
| `JogadorPulo`         | Gerencia pulos e detecção de chão com `OverlapCircle`      |
| `JogadorAnimador`     | Controla os parâmetros de animação com base nos estados    |
| `JogadorEstado`       | Controla vidas, queda e reinício da cena                   |
| `JogadorMeta`         | Detecta a meta da fase e faz transição de cena             |

---

## 🚀 Como rodar o projeto

1. Clone este repositório:
   ```bash
   git clone https://github.com/JogosIFPRTB/JogoPlataforma2d.git
  ´´´

2. Abra o projeto com o Unity 6.x (versão recomendada: 6000.0.0f1 ou superior).

3. Certifique-se de:
- A Layer "Chao" está criada e aplicada aos objetos de chão.
- O objeto PontoChao está posicionado corretamente no pé do jogador.
- O Animator Controller está configurado com os parâmetros:
  - Velocidade (float)
  - EstaNoChao (bool)
  - Pulo (trigger)

4. Pressione Play para testar o jogo.

---

## 🎨 Assets Utilizados

Este projeto utiliza os gráficos do pacote gratuito:

> **Pixel Adventure 1**  
> Disponível em: [https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360)

Créditos ao autor do pacote na Unity Asset Store. Os assets são utilizados apenas com finalidade educacional.

---

## 📦 Licença

Este projeto está licenciado sob a licença MIT. Sinta-se livre para estudar, modificar e distribuir.

---

## ✍️ Autoria

Desenvolvido e documentado com foco educacional por Luiz Carlos Pinheiro Junior (@frickajr).

---

## 🙌 Contribuições

Contribuições são bem-vindas!
Abra uma issue ou envie um pull request com melhorias, correções ou sugestões.

---

## 🙌 Contribuições

Contribuições são bem-vindas!
