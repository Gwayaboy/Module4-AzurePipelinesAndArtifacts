# Module 4: Running Functional Tests with Azure Pipeline & Artifacts

Please [view and download]() Module 4's Slide deck

## Agenda

 1.  **[Azure Pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/?view=azure-devops)**
     - [Repository](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/?view=azure-devops)
     - [Agents types](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/pools-queues?view=azure-devops&tabs=yaml%2Cbrowser
) 
     - [UI Testing consideration](https://docs.microsoft.com/en-us/azure/devops/pipelines/test/ui-testing-considerations?view=azure-devops&tabs=mstest)
     - Labs#1 : Create YAML Pipeline to build, release and run your functional UI Tests
  
 2. **Azure Artifacts**
    - [Overview](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/artifacts-overview?view=azure-devops) 
    - [Package](https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package) your test libary, [publish](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/nuget?view=azure-devops&tabs=yaml) and [share](https://docs.microsoft.com/en-us/azure/devops/pipelines/packages/nuget-restore?view=azure-devops)
 3. **Discussions & Wrap-up**
    - Q&A / Discussions
    - Wrap-up
 4. **Next steps**
    - [FeedBack for the session](https://aka.ms/PipelinesArtifacts)

    

## Hands-on Labs

  #### Building an Azure DevOps Pipeline for running Selenium Functional Tests

  1. Set up your Azure DevOps Organisation

      -	Use or [create](https://signup.live.com) your personal Microsoft Account (MSA)      
      -	[Create a free Azure DevOps organization](https://dev.azure.com/)  associated with your MSA

      - Create a Project by clicking on the top right corning New Project button 

        ![](https://demosta.blob.core.windows.net/images/NewDevOpsProject.PNG)
      
        - Set its visibility to public and name it AzurePipelineFunctionalTestsLab 
        - optionally provide with a description such as _"Build and run Web UI Functional Tests for Bing Web Search "_

  2. Import and setup GitHub Repo to Azure DevOps Repository
      - Click on the Repos on the left side Pane
      - Click on Import repository button to bring up **Import a Git repository** panel
      ![](https://demosta.blob.core.windows.net/images/ImportGitRepo.PNG)
        
        - Paste Module 2's repository [https://github.com/Gwayaboy/Module2-UIAutomationTesting.git](https://github.com/Gwayaboy/Module2-UIAutomationTesting.git) to clone
        - Click import button this will takes a few seconds to a minute until you get a succesfull repository message
          ![](https://demosta.blob.core.windows.net/images/ImportRepoSuccess.PNG)

        - From the top branch drop down switch to the Finished Branch

          ![](https://demosta.blob.core.windows.net/images/SelectFinishedBranch.PNG)


  3. Let's setup a new YAML by clicking on the top right ![](https://demosta.blob.core.windows.net/images/NewBuild.PNG)




        

