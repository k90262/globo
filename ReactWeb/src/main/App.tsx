import HouseList from '../house/HouseList'
import './App.css'
import Header from './Header'

function App() {
  return (
   <div className="container">
      <Header subtitle="Providing hourses all over the world" />
      <HouseList/>
   </div>
  )
}

export default App
