<h1 align="center">Serviço de Cotação de Moedas com Windows Service</h1>

<p align="center">Currency Value</p>

# Sumário

- [Descrição](#Descrição)
- [Desafios](#Desafios)
- [Features](#Features)
- [Tecnologias Utilizadas](#Tecnologias-Utilizadas)
- [Instalação](#Instalação)
- [Autor](#Autor)

# Descrição

Projeto desenvolvido como um desafio para ser entregue à empresa MXM Sistemas. O objetivo era criar um serviço do Sistema Operacional (Windows ou Linux) que enviasse e retornasse dados de uma API pública, apresentando o resultado em um arquivo de texto (txt) para o usuário.

Após análises e conversas com o cliente, foi definido que o projeto seria um Windows Service que faz requisições para 2 endpoints da API do Banco Central. As informações são processadas e apresentadas ao usuário. As atualizações são realizadas a cada 7 segundos (média de tempo para ver resultados). Um arquivo de texto é criado por dia para manter o registro, sendo sobrescrito ao longo do dia com as informações mais recentes fornecidas pela API. O arquivo é salvo na pasta "Cotações", junto com o binário, e é nomeado no formato "cotações dia-mês-ano". Durante feriados, finais de semana e antes das 10:30 nos dias úteis, não ocorrem atualizações, e as informações no texto aparecem como "0". O programa funciona corretamente mesmo com o arquivo aberto, carregando as novas informações assim que for fechado e reaberto.

Além disso, caso o projeto seja interrompido, outra pasta será criada contendo um arquivo de texto com informações genéricas, incluindo detalhes de quem contatar caso o usuário não tenha interrompido o serviço e confirmando a parada. Se ocorrer alguma exceção durante a gravação, um arquivo de texto será gerado na pasta "Logs", contendo o erro ocorrido.

# Features

- [x] Fazer uma Requisição para uma API escolhida (Banco Central);
- [x] Apresentar as informações recebidas em um txt para o usuário;
- [x] Apresentar mensagem informando hora e instruções ao usuário após o serviço ser parado.
- [x] Apresentar mensagem de erro caso ocorra algum problema durante a execução do serviço.

# Desafios

Durante o desenvolvimento do projeto, o maior desafio foi aprender a criar um Windows Service pela primeira vez e compreender seus conceitos e funcionamentos, a fim de garantir um bom desenvolvimento do desafio. Foi necessário assegurar que, mesmo ocorrendo erros, o projeto continuasse funcionando e apresentasse as devidas informações para o usuário. Além disso, foi essencial garantir um bom entendimento do problema do cliente e atender às suas expectativas e requisitos.

# Tecnologias Utilizadas

- Linguagem C#
- ASP.NET
- .NET Framework
- IDE Visual Studio
- POO (Programação Orientada a Objetos)

# Instalação

Abra o cmd e navegue até InstallUtil.exe em sua pasta .net; para o .net 4 é:

```cs
C:\Windows\Microsoft.NET\Framework\v4.0.30319
```

e use para instalar o serviço, passando o caminho do executável do projeto, exemplo:

```cs
InstallUtil.exe c:\program files\abc 123\myservice.exe
```

# Autor

<img src="https://avatars.githubusercontent.com/u/72415750?v=4" alt="ProfilePicture" title="ProfilePicture" width="200px" height="200px" />

[Felipe Augusto](https://github.com/ALCM-bit) &#128640;

Feito por Felipe Augusto L. C. Magalhães - Desenvolvedor Full Stack.
